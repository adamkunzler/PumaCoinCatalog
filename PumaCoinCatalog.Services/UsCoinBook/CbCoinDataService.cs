using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models.UsaCoinBook;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PumaCoinCatalog.UsCoinBook.Services
{
    public class CbCoinDataService
    {
        private readonly DataContext _context;

        public CbCoinDataService()
        {
            _context = new DataContext();
        }

        public CbCoinDataService(DataContext context)
        {
            _context = context;
        }

        #region CbCountry

        public IList<CbCountry> GetCountries()
        {
            var data = _context.CbCountries
                               .OrderBy(x => x.Title)
                               .ToList();
            return data;
        }

        public CbCountry GetCountry(string title = "USA")
        {
            var data = _context.CbCountries
                               .Include("Denominations")  
                               .Include("Denominations.Varieties")
                               .FirstOrDefault(x => x.Title == title);

            if (data == null) throw new Exception($"Country not found: {title}");

            return data;
        }

        #endregion CbCountry

        #region CbDenomination

        public CbDenomination GetDenomination(int denominationId)
        {
            var data = _context.CbDenominations
                               .Include("Varieties")
                               .FirstOrDefault(x => x.Id == denominationId);

            if (data == null) throw new Exception($"Denomination not found: {denominationId}");

            // sort everything
            data.Varieties = data.Varieties.OrderBy(x => x.Id).ToList();

            return data;
        }

        #endregion CbDenomination

        #region CbVariety

        public CbVariety GetVariety(int varietyId)
        {
            var data = _context.CbVarieties
                               .Include("Types")
                               .FirstOrDefault(x => x.Id == varietyId);

            if (data == null) throw new Exception($"Variety not found: {varietyId}");

            // sort everything
            data.Types = data.Types.OrderBy(x => x.BeginDate).ToList();

            return data;
        }
        
        public void UpdateVarietyTitle(int varietyId, string title)
        {
            var data = _context.CbVarieties.FirstOrDefault(x => x.Id == varietyId);
            if (data == null) throw new Exception($"Variety not found: {varietyId}");

            data.Title = title;

            _context.SaveChanges();
        }

        #endregion CbVariety

        #region CbType

        public CbType GetType(int typeId)
        {
            var data = _context.CbTypes
                               .Include("Coins")
                               .FirstOrDefault(x => x.Id == typeId);

            if (data == null) throw new Exception($"Type not found: {typeId}");

            // sort everything
            data.Coins = data.Coins
                             .OrderBy(x => x.Year)
                             .ThenBy(x => x.Details)
                             .ThenBy(x => x.MintMark)
                             .ToList();

            return data;                              
        }
        
        public void ChangeTypeObverseImageUri(int typeId, string uri)
        {
            var data = _context.CbTypes.FirstOrDefault(x => x.Id == typeId);
            if (data == null) throw new Exception($"Type not found: {typeId}");

            data.ObverseImageUri = uri;

            _context.SaveChanges();
        }

        public void ChangeTypeReverseImageUri(int typeId, string uri)
        {
            var data = _context.CbTypes.FirstOrDefault(x => x.Id == typeId);
            if (data == null) throw new Exception($"Type not found: {typeId}");

            data.ReverseImageUri = uri;

            _context.SaveChanges();
        }

        public void UpdateTypeMeltValue(int typeId, decimal meltValue)
        {
            var data = _context.CbTypes.FirstOrDefault(x => x.Id == typeId);
            if (data == null) throw new Exception($"Type not found: {typeId}");

            data.MeltValue = meltValue;

            _context.SaveChanges();
        }

        public void UpdateTypeTitle(int typeId, string title)
        {
            var data = _context.CbTypes.FirstOrDefault(x => x.Id == typeId);
            if (data == null) throw new Exception($"Type not found: {typeId}");

            data.Title = title;

            _context.SaveChanges();
        }

        public void UpdateDenominationTitle(int denominationId, string title)
        {
            var data = _context.CbDenominations.FirstOrDefault(x => x.Id == denominationId);
            if (data == null) throw new Exception($"Denomination not found: {denominationId}");

            data.Title = title;

            _context.SaveChanges();
        }
        #endregion CbType

        #region CbCoin

        public IList<CbCoin> GetAllCoins(int typeId)
        {
            var data = _context.CbCoins
                               .Where(x => x.Type.Id == typeId)
                               //.OrderBy(x => x.Year)
                               //.ThenBy(x => x.MintMark)
                               .ToList();
            return data;
        }

        public void AddNewCoin(int typeId, CbCoin newCoin)
        {
            var data = _context.CbTypes.FirstOrDefault(x => x.Id == typeId);
            if (data == null) throw new Exception($"Type not found: {typeId}");

            data.Coins.Add(newCoin);

            _context.SaveChanges();
        }

        #endregion CbCoin
    }
}
