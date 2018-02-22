using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models;
using System;
using System.Linq;

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
                                     //.Include("CoinCategories.CoinTypes")
                                     //.Include("CoinCategories.CoinTypes.Coins")
                                     .FirstOrDefault(x => x.Title == "U.S. Coins");
            if (collection == null) throw new Exception("US Coin collection not found");

            return collection;
        }

        public ScrapeCoinCategory GetCoinCategory(Guid id)
        {
            var category = _context.ScrapeCoinCategories
                                   .Include("CoinTypes")
                                   .Include("CoinCollection")
                                   .FirstOrDefault(x => x.Id == id);
            if (category == null) throw new Exception("Category (" + id + ") not found.");

            return category;
        }

        public ScrapeCoinType GetCoinType(Guid id)
        {
            var type = _context.ScrapeCoinTypes
                               .Include("Coins")
                               .Include("CoinCategory")
                               .Include("CoinCategory.CoinCollection")
                               .FirstOrDefault(x => x.Id == id);
            if (type == null) throw new Exception("Type (" + id + ") not found");

            return type;
        }
    }
}
