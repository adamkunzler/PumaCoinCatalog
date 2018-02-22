using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Services
{
    public class CoinDataService
    {
        private DataContext _context;

        public CoinDataService()
        {
            _context = new DataContext();
        }

        public ScrapeCoinCollection GetUsCoinCollection()
        {
            var collection = _context.ScrapeCoinCollections
                                     .Include("CoinCategories")
                                     .Include("CoinCategories.CoinTypes")
                                     .Include("CoinCategories.CoinTypes.Coins")
                                     .FirstOrDefault(x => x.Title == "U.S. Coins");
            if (collection == null) throw new Exception("US Coin collection not found");

            return collection;
        }
    }
}
