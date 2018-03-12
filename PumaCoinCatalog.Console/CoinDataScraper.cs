using CsQuery;
using Newtonsoft.Json;
using PumaCoinCatalog.Models;
using System.Linq;

namespace PumaCoinCatalog.Console
{
    public class CoinDataScraper : BaseDataScraper
    {
        public string ScrapeData()
        {
            var coinSort = 0;
            var typeSort = 0;
            var categorySort = 0;

            var baseUri = "https://www.uscoinlist.com";

            var coinCollection = new ScrapeCoinCollection { Title = "US Coins" };

            // get category html
            var categoryDom = GetHtml(baseUri);

            var divCategoryRows = categoryDom["#content .row > div"].ToList();            
            foreach (var divCategory in divCategoryRows)
            {
                var coinCategory = new ScrapeCoinCategory();
                CQ divCategoryHtml = divCategory.InnerHTML;

                // scrape category data
                coinCategory.Title = divCategoryHtml["a > b"].Text();
                coinCategory.Base64Image = ImageUtil.ConvertImageURLToBase64($"{baseUri}{divCategoryHtml["a > img"].Attr("src")}");
                coinCategory.SortOrder = categorySort++;
                var categoryUri = $"{baseUri}{divCategoryHtml["a"].Attr("href")}";

                // get type html
                var typeDom = GetHtml(categoryUri);
                var divTypeRows = typeDom["#content .row > div"].ToList();                
                foreach (var divType in divTypeRows)
                {
                    var coinType = new ScrapeCoinType();
                    CQ divTypeHtml = divType.InnerHTML;

                    // scrape type data
                    coinType.Title = divTypeHtml["a > b"].Text();
                    coinType.Details = CleanTypeDetails(divTypeHtml.Text(), coinType.Title);
                    coinType.Base64Image = ImageUtil.ConvertImageURLToBase64($"{baseUri}{divTypeHtml["a > img"].Attr("src")}");
                    coinType.SortOrder = typeSort++;
                    var typeUri = $"{baseUri}{divTypeHtml["a"].Attr("href")}";

                    // get coin html
                    var coinDom = GetHtml(typeUri);
                    var divCoinRows = coinDom["#content tbody > tr"].ToList();                    
                    foreach (var divCoin in divCoinRows)
                    {
                        var coin = new ScrapeCoin();
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
                        coin.SortOrder = coinSort++;

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

        

        #endregion Private Methods
    }
}
