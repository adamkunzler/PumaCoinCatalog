using Newtonsoft.Json;
using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models;
using PumaCoinCatalog.Models.UsaCoinBook;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PumaCoinCatalog.Console
{
    class Program
    {
        static void Main(string[] args)
        {            
            //LoadCoinData();
            //LoadCoinDataUsaCoinBook();

            //LoadSpecificTypes();

            ScrapeCoinImages();

            PressKeyToContinue();
        }

        

        private static void ScrapeCoinImages()
        {
            var scrapeImages = new ScrapeImages();
            scrapeImages.ScrapeVarietyImages();
            scrapeImages.ScrapeTypeImages();

            return;
        }

        private static void LoadSpecificTypes()
        {            
            var scraper = new CoinBookDataScraper();
            var context = new DataContext();

            var varieties = context.CbVarieties.Where(x => x.Title == "Two Cents" || x.Title == "Twenty Cents").ToList();

            foreach (var variety in varieties)
            {
                var types = scraper.ScrapeTypes(variety.SourceUri);
                variety.Types = types;
            }

            context.SaveChanges();
        }

        private static void LoadCoinDataUsaCoinBook()
        {
            var scraper = new CoinBookDataScraper();

            //var fromFile = true;
            //var jsonMenuData = fromFile ? GetData("usaCoinBookData_denomsAndTypes_20180312.json") : scraper.ScrapeMenu();

            //var denomsAndTypes = JsonConvert.DeserializeObject<List<CbScrapeMenuItem>>(jsonMenuData);
            //var jsonCoinData = scraper.ScrapeData(denomsAndTypes);

            var json = GetData("usaCoinBookData_coins_20180312.json");
            var coinDataCountry = JsonConvert.DeserializeObject<CbCountry>(json);

            var context = new DataContext();
            context.CbCountries.Add(coinDataCountry);
            context.SaveChanges();

            //System.Console.Write(scraper.OUTPUT);
            //System.Console.WriteLine("\n\n-------------------------------------------------------\n\n");
            //System.Console.Write(json);

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

        private static string GetData(string filename)
        {
            var data =  File.ReadAllText(filename);
            return data;
        }
    }
}
