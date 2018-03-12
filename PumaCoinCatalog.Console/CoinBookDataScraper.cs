using CsQuery;
using Newtonsoft.Json;
using PumaCoinCatalog.Models;
using PumaCoinCatalog.Models.UsaCoinBook;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PumaCoinCatalog.Console
{
    public class CoinBookDataScraper : BaseDataScraper
    {
        private readonly string _baseUri = "https://www.usacoinbook.com/";

        public string OUTPUT = "";

        public CoinBookDataScraper()
        {

        }

        public string ScrapeData(IList<CbScrapeMenuItem> denomsAndTypes)
        {
            var country = new CbCountry { Title = "United States of America" };
            
            var json = JsonConvert.SerializeObject(country, Formatting.Indented);
            return json;
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
