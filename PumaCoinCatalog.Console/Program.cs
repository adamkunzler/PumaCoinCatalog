using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CsQuery;
using PumaCoinCatalog.Models;
using Newtonsoft.Json;
using System.IO;

namespace PumaCoinCatalog.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var scraper = new CoinDataScraper();
            //var coinData = scraper.ScrapeData();

            var collection = LoadCoinData();

            PressKeyToContinue();
        }               
        
        private static CoinCollection LoadCoinData()
        {
            var json = GetData();
            var collection = JsonConvert.DeserializeObject<CoinCollection>(json);
            return collection;
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

        private static string GetData()
        {
            var data =  File.ReadAllText("coinData2018.json");
            return data;
        }
    }
}
