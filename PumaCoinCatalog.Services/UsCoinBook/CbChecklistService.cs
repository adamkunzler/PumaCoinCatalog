using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using PumaCoinCatalog.UsCoinBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Services.UsCoinBook
{
    public class CbChecklistService
    {
        private readonly DataContext _context;
        private readonly CbCoinDataService _coinDataService;

        public CbChecklistService()
        {
            _context = new DataContext();
            _coinDataService = new CbCoinDataService(_context);
        }

        public CbChecklistService(DataContext context)
        {
            _context = context;
            _coinDataService = new CbCoinDataService(context);
        }

        public IList<CbChecklist> GetChecklistByCollection(int collectionId)
        {
            var data = _context.CbChecklists.Where(x => x.Collection.Id == collectionId).ToList();
            return data;
        }

        public CbChecklist CreateChecklist(string title, int collectionId, int typeId)
        {
            // check if checklist already exists
            var exists = _context.CbChecklists.FirstOrDefault(x => x.Title == title);
            if (exists != null) throw new Exception($"Checklist with title '{title}' already exists.");

            var checklist = new CbChecklist
            {
                Title = title,
                LastModified = DateTime.UtcNow,
                Collection = _context.CbCollections.Single(x => x.Id == collectionId),
                Type = _context.CbTypes.Single(x => x.Id == typeId)                
            };

            checklist.Coins = CreateChecklistCoinsForNewChecklist(checklist);

            _context.CbChecklists.Add(checklist);
            _context.SaveChanges();

            return checklist;
        }

        private IList<CbChecklistCoin> CreateChecklistCoinsForNewChecklist(CbChecklist checklist)
        {
            var checklistCoins = new List<CbChecklistCoin>();
            
            var cbCoins = _coinDataService.GetAllCoins(checklist.Type.Id);
            foreach (var cbCoin in cbCoins)
            {
                var coin = new CbChecklistCoin
                {
                    Coin = cbCoin,
                    Grade = Models.UsaCoinBook.CbGrade.NotGraded,
                    Checklist = checklist,
                    InCollection = false,
                    ShouldExclude = false,
                    ValueEstimate = 0            
                };
                checklistCoins.Add(coin);
            }

            return checklistCoins;
        }
    }
}
