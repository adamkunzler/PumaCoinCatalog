using LazyCache;
using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PumaCoinCatalog.Services
{
    public class CoinDataService
    {
        private readonly DataContext _context;
        private readonly IAppCache _cache;

        public CoinDataService()
        {
            _context = new DataContext();
            _cache = new CachingService();
        }

        public CoinDataService(DataContext context)
        {
            _context = context;
        }
        
        public ScrapeCoinCollection GetUsCoinCollection()
        {
            var key = "Collection_UsCoins";

            var collection = _cache.GetOrAdd(key, () =>
            {
                return _context.ScrapeCoinCollections
                        .Include("CoinCategories")
                        .FirstOrDefault(x => x.Title == "U.S. Coins");
            });

            if (collection == null) throw new Exception("US Coin collection not found");

            return collection;
        }

        public ScrapeCoinCategory GetCoinCategory(Guid id)
        {
            var key = $"CoinCategory_{id}";

            var category = _cache.GetOrAdd(key, () =>
            {
                return _context.ScrapeCoinCategories
                                   .Include("CoinTypes")
                                   .Include("CoinCollection")
                                   .FirstOrDefault(x => x.Id == id);
            });

            if (category == null) throw new Exception("Category (" + id + ") not found.");

            return category;
        }

        public ScrapeCoinType GetCoinType(Guid id)
        {
            var key = $"CoinType_{id}";

            var type = _cache.GetOrAdd(key, () =>
            {
                return _context.ScrapeCoinTypes
                               .Include("Coins")
                               .Include("CoinCategory")
                               .Include("CoinCategory.CoinCollection")
                               .FirstOrDefault(x => x.Id == id);
            });
            if (type == null) throw new Exception("Type (" + id + ") not found");

            return type;
        }

        public IList<ScrapeCoinCollection> GetAllCollections()
        {
            var key = "Collection_All";
            var collections = _context.ScrapeCoinCollections.ToList();
            return collections;
        }

        public IList<ScrapeCoinCategory> GetAllCategoriesByCollection(Guid collectionId)
        {
            var key = $"Categories_All_{collectionId}";

            var categories = _cache.GetOrAdd(key, () =>
            {
                return _context.ScrapeCoinCategories
                                     .Where(x => x.CoinCollection.Id == collectionId)
                                     .OrderBy(x => x.SortOrder)
                                     .ToList();
            });

            return categories;
        }

        public IList<ScrapeCoinType> GetAllTypesByCategory(Guid categoryId)
        {
            var key = $"Types_All_{categoryId}";

            var types = _cache.GetOrAdd(key, () =>
            {
                return _context.ScrapeCoinTypes
                                .Where(x => x.CoinCategory.Id == categoryId)
                                .OrderBy(x => x.SortOrder)
                                .ToList();
            });

            return types;
        }

        public IList<ScrapeCoin> GetAllCoinsByType(Guid typeId)
        {
            var key = $"Coins_All_{typeId}";

            var coins = _cache.GetOrAdd(key, () =>
            {
                return _context.ScrapeCoins
                                .Where(x => x.CoinType.Id == typeId)
                                .OrderBy(x => x.SortOrder)
                                .ToList();
            });

            return coins;
        }

        public void UpdateCoinTypeBullionValue(Guid coinTypeId, decimal bullionValue)
        {
            var coinType = _context.ScrapeCoinTypes.SingleOrDefault(x => x.Id == coinTypeId);
            if (coinType == null) throw new Exception("coinType is not found");

            coinType.BullionValue = bullionValue;
            _context.SaveChanges();

            _cache.Remove($"CoinType_{coinTypeId}");            
        }
    }
}
