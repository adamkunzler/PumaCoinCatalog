using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models;
using PumaCoinCatalog.Models.UsaCoinBook;
using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using PumaCoinCatalog.UsCoinBook.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        
        public CbChecklist CreateChecklist(string title, int collectionId, int typeId)
        {
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

        public void AddCoinToChecklist(int checklistCoinId)
        {
            var checklistCoin = _context.CbChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("CbChecklistCoin not found: " + checklistCoinId);

            checklistCoin.InCollection = true;

            _context.SaveChanges();
        }

        public void RemoveCoinFromChecklist(int checklistCoinId)
        {
            var checklistCoin = _context.CbChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("CbChecklistCoin not found: " + checklistCoinId);

            checklistCoin.InCollection = false;

            _context.SaveChanges();
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
                    Grade = CbGrade.NotGraded,
                    Checklist = checklist,
                    InCollection = false,
                    ShouldExclude = false,
                    ValueEstimate = 0            
                };
                checklistCoins.Add(coin);
            }

            return checklistCoins;
        }

        public void SetChecklistCoinExlude(int checklistCoinId, bool v)
        {
            var checklistCoin = _context.CbChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("CbChecklistCoin not found: " + checklistCoinId);

            checklistCoin.ShouldExclude = v;

            _context.SaveChanges();
        }

        public void UpdateChecklistCoinGrade(int checklistCoinId, CbGrade grade)
        {
            var checklistCoin = _context.CbChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("CbChecklistCoin not found: " + checklistCoinId);

            checklistCoin.Grade = grade;

            _context.SaveChanges();
        }

        public void UpdateChecklistCoinEstimatedValue(int checklistCoinId, decimal value)
        {
            var checklistCoin = _context.CbChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("CbChecklistCoin not found: " + checklistCoinId);

            checklistCoin.ValueEstimate = value;

            _context.SaveChanges();
        }

        public void UpdateChecklistCoinQuantity(int checklistCoinId, int quantity)
        {
            var checklistCoin = _context.CbChecklistCoins.SingleOrDefault(x => x.Id == checklistCoinId);
            if (checklistCoin == null) throw new Exception("CbChecklistCoin not found: " + checklistCoinId);

            checklistCoin.Quantity = quantity;

            _context.SaveChanges();
        }

        public void UpdateChecklistTitle(int checklistId, string title)
        {
            var data = _context.CbChecklists.SingleOrDefault(x => x.Id == checklistId);
            if (data == null) throw new Exception($"Checklist not found: id = {checklistId}");

            data.Title = title;

            _context.SaveChanges();
        }

        public CbChecklist GetChecklist(int checklistId)
        {
            var data = _context.CbChecklists
                               .Include("Collection")
                               .Include("Type")
                               .Include("Type.Variety")
                               .Include("Type.Variety.Denomination")
                               .SingleOrDefault(x => x.Id == checklistId);
            if (data == null) throw new Exception($"Checklist not found: id = {checklistId}");
            return data;
        }

        public void DeleteChecklist(int checklistId)
        {
            var checklist = _context.CbChecklists.SingleOrDefault(x => x.Id == checklistId);
            if (checklist == null) throw new Exception("checklist not found: " + checklistId);

            var checklistCoins = _context.CbChecklistCoins.Where(x => x.Checklist.Id == checklistId);

            _context.CbChecklistCoins.RemoveRange(checklistCoins);
            _context.CbChecklists.Remove(checklist);

            _context.SaveChanges();
        }

        public CbChecklistValueSummary GetChecklistValueSummary(int checklistId)
        {            
            var idParam = new SqlParameter
            {
                ParameterName = "ChecklistId",
                Value = checklistId
            };

            var summary = _context.Database
                                .SqlQuery<CbChecklistValueSummary>("exec GetChecklistValueSummary @ChecklistId", idParam)
                                .FirstOrDefault();            
            return summary;
        }        
    }
}
