using LazyCache;
using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Services
{
    public class ChecklistService
    {
        private readonly DataContext _context;
        private readonly CoinDataService _coinDataService;
        private readonly IAppCache _cache;

        public ChecklistService()
        {
            _context = new DataContext();
            _coinDataService = new CoinDataService(_context);
            _cache = new CachingService();
        }

        public ChecklistService(DataContext context)
        {
            _context = context;
            _coinDataService = new CoinDataService(_context);
            _cache = new CachingService();
        }

        public IList<Checklist> GetAllChecklists()
        {
            var key = "Checklists_All";

            var checklists = _cache.GetOrAdd(key, () =>
            {
                return _context.Checklists
                                     .Include("CoinCollection")
                                     .Include("CoinCategory")
                                     .Include("CoinType")
                                     .OrderBy(x => x.CoinCategory.FaceValue)
                                     .ToList();
            });

            return checklists;
        }

        public Checklist GetChecklist(Guid checklistId)
        {
            var key = $"Checklist_{checklistId}";

            var checklist = _cache.GetOrAdd(key, () =>
            {
                return _context.Checklists
                                    .Include("CoinCollection")
                                    .Include("CoinCategory")
                                    .Include("CoinType")
                                    .Include("ChecklistCoins")
                                    .Include("ChecklistCoins.Coin")
                                    .FirstOrDefault(x => x.Id == checklistId);
            });

            return checklist;
                                    
        }

        public Guid CreateNewChecklist(string title, Guid collectionId, Guid categoryId, Guid typeId)
        {
            var checklist = new Checklist();
            checklist.Title = title;
            checklist.LastModified = DateTime.UtcNow;
            checklist.CoinCollection = _context.ScrapeCoinCollections.First(x => x.Id == collectionId);
            checklist.CoinCategory = _context.ScrapeCoinCategories.First(x => x.Id == categoryId);
            checklist.CoinType = _context.ScrapeCoinTypes.First(x => x.Id == typeId);            
            checklist.ChecklistCoins = CreateChecklistCoinsForNewChecklist(checklist);

            _context.Checklists.Add(checklist);
            _context.SaveChanges();

            _cache.Remove("Checklists_All");
            _cache.Remove($"Checklist_{checklist.Id}");

            return checklist.Id;
        }

        public void UpdateChecklistCoinGrade(Guid checklistCoinId, short value)
        {
            var checklistCoin = _context.ChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("checklist coin not found");

            checklistCoin.InCollection = true;
            checklistCoin.AdamGrade = value;
            _context.SaveChanges();

            _cache.Remove("Checklists_All");
            _cache.Remove($"Checklist_{checklistCoin.Checklist.Id}");
        }        

        public void DeleteChecklist(Guid checklistId)
        {
            var checklist = _context.Checklists.SingleOrDefault(x => x.Id == checklistId);
            if (checklist == null) throw new Exception("checklist not found");

            var checklistCoins = _context.ChecklistCoins.Where(x => x.Checklist.Id == checklistId);

            _context.ChecklistCoins.RemoveRange(checklistCoins);
            _context.Checklists.Remove(checklist);
            _context.SaveChanges();

            _cache.Remove("Checklists_All");
            _cache.Remove($"Checklist_{checklist.Id}");
        }

        public void UpdateChecklistCoinInCollection(Guid checklistCoinId, bool value)
        {
            var checklistCoin = _context.ChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("checklist coin not found");

            checklistCoin.InCollection = value;

            if(!value)
            {
                checklistCoin.AdamGrade = null;
                checklistCoin.ValueEstimate = null;
            }

            _context.SaveChanges();

            _cache.Remove("Checklists_All");
            _cache.Remove($"Checklist_{checklistCoin.Checklist.Id}");
        }

        public void UpdateChecklistCoinEstimatedValue(Guid checklistCoinId, decimal value)
        {
            var checklistCoin = _context.ChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("checklist coin not found");

            checklistCoin.InCollection = true;
            checklistCoin.ValueEstimate = value;
            _context.SaveChanges();

            _cache.Remove("Checklists_All");
            _cache.Remove($"Checklist_{checklistCoin.Checklist.Id}");
        }

        private IList<ChecklistCoin> CreateChecklistCoinsForNewChecklist(Checklist checklist)
        {
            var checklistCoins = new List<ChecklistCoin>();

            var scrapeCoins = _coinDataService.GetAllCoinsByType(checklist.CoinType.Id);
            foreach (var scrapeCoin in scrapeCoins)
            {
                var coin = new ChecklistCoin
                {                    
                    Coin = scrapeCoin,
                    InCollection = false,
                    AdamGrade = null,
                    ValueEstimate = null
                };
                checklistCoins.Add(coin);
            }

            return checklistCoins;
        }
    }
}
