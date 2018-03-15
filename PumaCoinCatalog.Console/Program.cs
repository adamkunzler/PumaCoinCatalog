using Newtonsoft.Json;
using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models;
using PumaCoinCatalog.Models.UsaCoinBook;
using System.Collections.Generic;
using System.IO;

namespace PumaCoinCatalog.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var scraper = new CoinDataScraper();
            //var coinData = scraper.ScrapeData();

            //LoadCoinData();
            //LoadCoinDataUsaCoinBook();
            
            PressKeyToContinue();
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

        private static void LoadCoinData()
        {
            var json = GetData("coinData2018.json");
            var collection = JsonConvert.DeserializeObject<ScrapeCoinCollection>(json);

            var context = new DataContext();
            context.ScrapeCoinCollections.Add(collection);
            context.SaveChanges();
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
