using CsQuery;
using Newtonsoft.Json;
using PumaCoinCatalog.Models;
using PumaCoinCatalog.Models.UsaCoinBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PumaCoinCatalog.Console
{
    public class CoinBookDataScraper : BaseDataScraper
    {
        private readonly string _baseUri = "https://www.usacoinbook.com/";
        private decimal _currentFaceValue;
        public string OUTPUT = "";

        public CoinBookDataScraper()
        {

        }

        public string ScrapeData(IList<CbScrapeMenuItem> denomsAndTypes)
        {
            var country = new CbCountry
            {
                Title = "United States of America",
                Denominations = new List<CbDenomination>()
            };

            foreach(var denom in denomsAndTypes)
            {
                var denomination = new CbDenomination
                {
                    Title = denom.Title,
                    SourceUri = denom.Uri,
                    FaceValue = GetFaceValue(denom.Title),
                    Varieties = new List<CbVariety>()
                };

                _currentFaceValue = denomination.FaceValue;

                foreach(var item in denom.Items)
                {
                    var variety = new CbVariety
                    {
                        Title = item.Title,
                        SourceUri = item.Uri,
                        //Types = item.Title == "Washington" || item.Title == "Morgan" || item.Title == "Lincoln Wheat Cent" ? ScrapeTypes(item.Uri) : new List<CbType>()
                        Types = ScrapeTypes(item.Uri)
                    };

                    denomination.Varieties.Add(variety);
                }

                country.Denominations.Add(denomination);
            }
            
            var json = JsonConvert.SerializeObject(country, Formatting.Indented);
            return json;
        }        

        public IList<CbType> ScrapeTypes(string uri)
        {
            System.Threading.Thread.Sleep(1000);

            var types = new List<CbType>();

            var html = GetHtml(uri);

            // get all divs (they alternate between type info and coins) and remove ones we don't want
            var divs = html["div.content > div"].ToList();
            divs.RemoveAt(0);
            //divs.RemoveRange(divs.Count() - 5, 5);

            for(var i = 0; i < divs.Count(); i++)
            {
                var miClass = divs[i].GetAttribute("style");
                if (!miClass.Contains("text-align:")) continue;

                CQ htmlDiv = divs[i].InnerHTML;
                
                // get title and date range
                var h2 = htmlDiv["h2"].FirstOrDefault();
                GetTypeTitleAndDates(h2.InnerText, out string title, out short beginDate, out short endDate);

                // check if type is already added
                i++; // increment to next div that has coin table
                var exist = types.FirstOrDefault(x => x.Title == title && x.BeginDate == beginDate && x.EndDate == endDate);
                if (exist == null)
                {
                    // get specs
                    var specDivs = htmlDiv["div.coin-table-specs"].ToList();
                    GetTypeSpecs(specDivs, out string metalComposition, out string diameter, out string mass);

                    // create type

                    var type = new CbType
                    {
                        Title = title,
                        MetalComposition = metalComposition,
                        Diameter = GetCleanMeasurement(diameter),
                        Mass = GetCleanMeasurement(mass),
                        BeginDate = beginDate,
                        EndDate = endDate,
                        MeltValue = 0m,
                        Coins = ScrapeCoins(divs[i])
                    };
                    types.Add(type);
                }
                else
                {
                    // type already exists...just get coins
                    var coins = ScrapeCoins(divs[i]);
                    foreach (var coin in coins) exist.Coins.Add(coin);
                }                
            }

            return types;
        }

        private IList<CbCoin> ScrapeCoins(IDomObject parentDiv)
        {
            var coins = new List<CbCoin>();

            CQ html = parentDiv.InnerHTML;
            var coinRows = html["table > tbody > tr"].ToList();
            foreach (var coinRow in coinRows)
            {
                CQ coinHtml = coinRow.InnerHTML;

                // scrape coin data
                var tdCoinData = coinHtml.Elements.ToList();
                var rawYear = tdCoinData[0].FirstChild.FirstChild.NodeValue;
                var year = rawYear.Substring(0, 4);
                var mintMark = rawYear.Length > 5 ? rawYear.Substring(5, 1) : "";
                var details = tdCoinData[1].InnerText;
                var mintage = tdCoinData[2].InnerText;
                var gradeG4 = tdCoinData[3].InnerText;
                var gradeVG8 = tdCoinData[4].InnerText;
                var gradeF12 = tdCoinData[5].InnerText;
                var gradeVF20 = tdCoinData[6].InnerText;
                var gradeEF40 = tdCoinData[7].InnerText;
                var gradeAU50 = tdCoinData[8].InnerText;
                var gradeMS60 = tdCoinData[9].InnerText;
                var gradeMS63 = tdCoinData[10].InnerText;
                var gradePr65 = tdCoinData[11].InnerText;

                // create coin
                var coin = new CbCoin
                {
                    Year = int.Parse(year),
                    MintMark = mintMark,
                    Details = details,
                    Mintage = GetMintage(mintage),
                    GradeValues = new List<CbGradeValue>
                    {
                        GetGradeValue(CbGrade.Good, gradeG4),
                        GetGradeValue(CbGrade.VeryGood, gradeVG8),
                        GetGradeValue(CbGrade.Fine, gradeF12),
                        GetGradeValue(CbGrade.VeryFine, gradeVF20),
                        GetGradeValue(CbGrade.ExtraFine, gradeEF40),
                        GetGradeValue(CbGrade.AU, gradeAU50),
                        GetGradeValue(CbGrade.MS60, gradeMS60),
                        GetGradeValue(CbGrade.MS63, gradeMS63),
                        GetGradeValue(CbGrade.Proof, gradePr65),
                    }
                };
                coins.Add(coin);
            }

            return coins;
        }

        public string ScrapeMenu()
        {
            var sb = new StringBuilder();
            var scrapedData = new List<CbScrapeMenuItem>();
            
            var homepageDom = GetHtml(_baseUri);
                                    
            // get all menu item anchors and remove the ones we don't want
            var htmlMenuItems = homepageDom["#sidebar a.list-group-item"].ToList();            
            htmlMenuItems.RemoveRange(0, 4);
            htmlMenuItems.RemoveRange(htmlMenuItems.Count() - 42, 42);

            const string classDenom = "sidebar-coindem-link";
            const string classType = "sidebar-cointype-link";            
            for(var i = 0; i < htmlMenuItems.Count(); i++)
            {
                var menuItemDenom = htmlMenuItems[i];

                // make sure we're on denom
                var miClass = menuItemDenom.GetAttribute("class");
                if (!miClass.Contains(classDenom)) throw new System.Exception("not on denom!");

                // get denom data
                var denomTitle = menuItemDenom.FirstChild.InnerText;
                var denomUri = menuItemDenom.GetAttribute("href");
                sb.AppendLine(denomTitle + "\t" + denomUri + "\t" + GetFaceValue(denomTitle));

                // create the denom
                var denom = new CbScrapeMenuItem
                {
                    Title = denomTitle,
                    Uri = denomUri,
                    Items = new List<CbScrapeMenuItem>()
                };

                for(var j = i + 1; j < htmlMenuItems.Count(); j++, i++)
                {
                    var menuItemType = htmlMenuItems[j];

                    // make sure we're on type
                    var miClass2 = menuItemType.GetAttribute("class");
                    if(!miClass2.Contains(classType))
                    {
                        break;
                    }

                    // get type data

                    var typeTitle = menuItemType.FirstChild.NodeValue;
                    var typeUri = menuItemType.GetAttribute("href");
                    sb.AppendLine("\t" + typeTitle + "\t" + typeUri);

                    // create the type
                    var type = new CbScrapeMenuItem
                    {
                        Title = typeTitle,
                        Uri = typeUri,
                        Items = new List<CbScrapeMenuItem>()
                    };
                    denom.Items.Add(type);
                }

                scrapedData.Add(denom);
            }           

            OUTPUT = sb.ToString();

            var json = JsonConvert.SerializeObject(scrapedData, Formatting.Indented);
            return json;
        }
        
        private long GetMintage(string value)
        {
            long.TryParse(value.Replace(",", ""), out long mintage);
            return mintage;
        }

        private CbGradeValue GetGradeValue(CbGrade grade, string value)
        {
            var str = value.Replace(",", "").Trim();
            var gradeValue = 0m;
            if (!decimal.TryParse(str, out gradeValue)) gradeValue = _currentFaceValue;

            return new CbGradeValue
            {
                Grade = grade,
                Value = gradeValue
            };
        }

        private float GetCleanMeasurement(string raw)
        {
            return float.Parse(Regex.Replace(raw, "[^0-9.]", ""));
        }

        private void GetTypeTitleAndDates(string raw, out string title, out short beginDate, out short endDate)
        {
            var indexLeft = raw.IndexOf("(");
            var indexRight = raw.LastIndexOf(")");
            title = raw.Substring(0, indexLeft - 1).Trim();

            var dates = raw.Substring(indexLeft).Split('-');
            if (dates.Length == 1)
            {
                // remove post and pre parentheses
                beginDate = short.Parse(dates[0].Trim().Remove(dates[0].Length - 1, 1).Remove(0, 1));
                endDate = beginDate;
            }
            else
            {
                beginDate = short.Parse(dates[0].Trim().Remove(0, 1));
                var strEndDate = dates[1].Trim().Remove(dates[1].Length - 1, 1);
                if (strEndDate == "Present" || strEndDate == "Current") strEndDate = DateTime.UtcNow.Year.ToString();
                if (strEndDate.Length > 4) strEndDate = strEndDate.Substring(0, 4); // susan b end date is 1981 + 1999

                endDate = short.Parse(strEndDate);
            }
        }

        private void GetTypeSpecs(IList<IDomObject> divs, out string metalComposition, out string diameter, out string mass)
        {
            var start = 0;
            if (divs.Count() > 4) start = 1;

            metalComposition = divs[start + 1].LastChild.NodeValue;
            diameter = divs[start + 2].LastChild.NodeValue;
            mass = divs[start + 3].LastChild.NodeValue;
        }

        private decimal GetFaceValue(string denomination)
        {
            switch (denomination)
            {
                case "Half Cents":
                    return .005m;
                case "Large Cents":
                case "Small Cents":
                    return .01m;
                case "Two Cents":
                    return .02m;
                case "Three Cents":
                    return .03m;
                case "Half Dimes":
                case "Nickels":
                    return .05m;
                case "Dimes":
                    return .1m;
                case "Twenty Cents":
                    return .2m;
                case "Quarters":
                    return .25m;
                case "Half Dollars":
                    return .5m;
                case "Dollars":
                case "Gold Dollars":
                    return 1m;
                case "Gold $2.50 Quarter Eagle":
                    return 2.5m;
                case "Gold $3":
                    return 3m;
                case "Gold $4":
                    return 4m;
                case "Gold $5 Half Eagle":
                    return 5m;
                case "Gold $10 Eagle":
                    return 10m;
                case "Gold $20 Double Eagle":
                    return 20m;
                default: throw new System.Exception("Unknown denomination");
            }
        }
    }

    public class CbScrapeMenuItem
    {
        public string Title { get; set; }
        public string Uri { get; set; }        
        public IList<CbScrapeMenuItem> Items { get; set; }

        public override string ToString()
        {
            return $"{Title} ({Items.Count()})";
        }
    }
}
