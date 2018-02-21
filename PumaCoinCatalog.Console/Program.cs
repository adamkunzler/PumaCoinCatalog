using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CsQuery;
using PumaCoinCatalog.Models;
using Newtonsoft.Json;

namespace PumaCoinCatalog.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var coinData = ScrapeCoinData();
            Log(coinData);

            PressKeyToContinue();
        }

        private static string ScrapeCoinData()
        {
            var baseUri = "https://www.uscoinlist.com";

            var coinCollection = new CoinCollection { Title = "US Coins" };

            // get category html
            var categoryDom = GetHtml(baseUri);            
                        
            var divCategoryRows = categoryDom["#content .row > div"].ToList();
            foreach(var divCategory in divCategoryRows)
            {
                var coinCategory = new CoinCategory();
                CQ divCategoryHtml = divCategory.InnerHTML;

                // scrape category data
                coinCategory.Title= divCategoryHtml["a > b"].Text();                
                coinCategory.ImageUrl = $"{baseUri}{divCategoryHtml["a > img"].Attr("src")}";
                var categoryUri = $"{baseUri}{divCategoryHtml["a"].Attr("href")}";

                // get type html
                var typeDom = GetHtml(categoryUri);
                var divTypeRows = typeDom["#content .row > div"].ToList();
                foreach(var divType in divTypeRows)
                {
                    var coinType = new CoinType();
                    CQ divTypeHtml = divType.InnerHTML;

                    // scrape type data
                    coinType.Title = divTypeHtml["a > b"].Text();
                    coinType.Details = CleanTypeDetails(divTypeHtml.Text(), coinType.Title);
                    coinType.ImageUri = $"{baseUri}{divTypeHtml["a > img"].Attr("src")}";
                    var typeUri = $"{baseUri}{divTypeHtml["a"].Attr("href")}";

                    // get coin html
                    var coinDom = GetHtml(typeUri);
                    var divCoinRows = coinDom["#content tbody > tr"].ToList();
                    foreach(var divCoin in divCoinRows)
                    {
                        var coin = new Coin();
                        CQ divCoinHtml = divCoin.InnerHTML;

                        // scrape coin data
                        var tdCoinData = divCoinHtml.Elements.ToList();
                        coin.Year = tdCoinData[0].InnerText;
                        coin.Variety = tdCoinData[1].InnerText;
                        coin.Mintage = CleanCoinMintage(tdCoinData[2].InnerText);
                        coin.KmNumber = tdCoinData[3].InnerText;
                        coin.NumisMediaId = GetCoinIntId(tdCoinData[4].InnerHTML);
                        coin.NgcId = GetCoinIntId(tdCoinData[5].InnerHTML);
                        coin.PcgsId = GetCoinIntId(tdCoinData[6].InnerHTML);

                        coinType.Coins.Add(coin);
                    }

                    coinCategory.CoinTypes.Add(coinType);
                }

                coinCollection.CoinCategories.Add(coinCategory);
            }

            var json = JsonConvert.SerializeObject(coinCollection, Formatting.Indented);
            return json;
        }

        private static int GetCoinIntId(CQ dom)
        {
            var a = dom["a"].Text();
            var id = string.IsNullOrWhiteSpace(a) ? 0 : int.Parse(a);
            return id;
        }

        private static int CleanCoinMintage(string dirty)
        {
            var strClean = dirty.Replace(",", "").Replace("-", "");
            var clean = string.IsNullOrWhiteSpace(strClean) ? 0 : int.Parse(strClean);
            return clean;
        }

        private static string CleanTypeDetails(string dirty, string toRemove)
        {
            var clean = dirty.Replace("\n", " ");
            clean = clean.Replace(toRemove, "");
            clean = clean.Trim();
            return clean;
        }

        private static CQ GetHtml(string uri)
        {
            CQ dom;

            using (var client = new WebClient())
            {
                dom = client.DownloadString(uri);
            }

            return dom;
        }
        
        private static void Log(string msg)
        {
            System.Console.WriteLine(msg);
        }

        private static void PressKeyToContinue()
        {
            System.Console.WriteLine("\n\nPress Enter key to continue");
            System.Console.ReadLine();
        }
    }
}
