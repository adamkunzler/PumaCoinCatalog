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

        public ChecklistService()
        {
            _context = new DataContext();
            _coinDataService = new CoinDataService(_context);
        }

        public ChecklistService(DataContext context)
        {
            _context = context;
            _coinDataService = new CoinDataService(_context);
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
            
            return checklist.Id;
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
