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

        public IList<Checklist> GetAllChecklists()
        {
           
            var checklists = _context.Checklists
                                     .Include("CoinCollection")
                                     .Include("CoinCategory")
                                     .Include("CoinType")
                                     .OrderBy(x => x.CoinCategory.FaceValue)
                                     .ToList();           

            return checklists;
        }

        public Checklist GetChecklist(Guid checklistId)
        {            
            var checklist = _context.Checklists
                                    .Include("CoinCollection")
                                    .Include("CoinCategory")
                                    .Include("CoinType")
                                    .Include("ChecklistCoins")
                                    .Include("ChecklistCoins.Coin")
                                    .FirstOrDefault(x => x.Id == checklistId);         

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
            
            return checklist.Id;
        }

        public void UpdateChecklistCoinGrade(Guid checklistCoinId, short value)
        {
            var checklistCoin = _context.ChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("checklist coin not found");

            checklistCoin.InCollection = true;
            checklistCoin.AdamGrade = value;
            _context.SaveChanges();
        }        

        public void RenameChecklist(Guid checklistId, string checklistTitle)
        {
            var checklist = _context.Checklists.SingleOrDefault(x => x.Id == checklistId);
            if (checklist == null) throw new Exception("checklist not found");

            checklist.Title = checklistTitle;
            _context.SaveChanges();

        }

        public void DeleteChecklist(Guid checklistId)
        {
            var checklist = _context.Checklists.SingleOrDefault(x => x.Id == checklistId);
            if (checklist == null) throw new Exception("checklist not found");

            var checklistCoins = _context.ChecklistCoins.Where(x => x.Checklist.Id == checklistId);

            _context.ChecklistCoins.RemoveRange(checklistCoins);
            _context.Checklists.Remove(checklist);
            _context.SaveChanges();
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
        }

        public void UpdateChecklistCoinEstimatedValue(Guid checklistCoinId, decimal value)
        {
            var checklistCoin = _context.ChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("checklist coin not found");

            checklistCoin.InCollection = true;
            checklistCoin.ValueEstimate = value;
            _context.SaveChanges();
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
