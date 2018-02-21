using System.Linq;
using System.Net;
using CsQuery;
using PumaCoinCatalog.Models;
using Newtonsoft.Json;

namespace PumaCoinCatalog.Console
{
    public class CoinDataScraper
    {
        public string ScrapeData()
        {
            var baseUri = "https://www.uscoinlist.com";

            var coinCollection = new RawCoinCollection { Title = "US Coins" };

            // get category html
            var categoryDom = GetHtml(baseUri);

            var divCategoryRows = categoryDom["#content .row > div"].ToList();
            foreach (var divCategory in divCategoryRows)
            {
                var coinCategory = new RawCoinCategory();
                CQ divCategoryHtml = divCategory.InnerHTML;

                // scrape category data
                coinCategory.Title = divCategoryHtml["a > b"].Text();
                coinCategory.ImageUrl = $"{baseUri}{divCategoryHtml["a > img"].Attr("src")}";
                var categoryUri = $"{baseUri}{divCategoryHtml["a"].Attr("href")}";

                // get type html
                var typeDom = GetHtml(categoryUri);
                var divTypeRows = typeDom["#content .row > div"].ToList();
                foreach (var divType in divTypeRows)
                {
                    var coinType = new RawCoinType();
                    CQ divTypeHtml = divType.InnerHTML;

                    // scrape type data
                    coinType.Title = divTypeHtml["a > b"].Text();
                    coinType.Details = CleanTypeDetails(divTypeHtml.Text(), coinType.Title);
                    coinType.ImageUri = $"{baseUri}{divTypeHtml["a > img"].Attr("src")}";
                    var typeUri = $"{baseUri}{divTypeHtml["a"].Attr("href")}";

                    // get coin html
                    var coinDom = GetHtml(typeUri);
                    var divCoinRows = coinDom["#content tbody > tr"].ToList();
                    foreach (var divCoin in divCoinRows)
                    {
                        var coin = new RawCoin();
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

        #region Private Methods

        private int GetCoinIntId(CQ dom)
        {
            var a = dom["a"].Text();
            var id = string.IsNullOrWhiteSpace(a) ? 0 : int.Parse(a);
            return id;
        }

        private int CleanCoinMintage(string dirty)
        {
            var strClean = dirty.Replace(",", "").Replace("-", "");
            var clean = string.IsNullOrWhiteSpace(strClean) ? 0 : int.Parse(strClean);
            return clean;
        }

        private string CleanTypeDetails(string dirty, string toRemove)
        {
            var clean = dirty.Replace("\n", " ");
            clean = clean.Replace(toRemove, "");
            clean = clean.Trim();
            return clean;
        }

        private CQ GetHtml(string uri)
        {
            CQ dom;

            using (var client = new WebClient())
            {
                dom = client.DownloadString(uri);
            }

            return dom;
        }

        #endregion Private Methods
    }
}
