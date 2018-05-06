using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models.UsaCoinBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Services.UsCoinBook
{
    public class CbGeneralService
    {
        private readonly DataContext _context;

        public CbGeneralService()
        {
            _context = new DataContext();
        }

        public CbGeneralService(DataContext context)
        {
            _context = context;
        }

        public IList<CbCountry> GetAllCountries()
        {
            var data = _context.CbCountries.ToList();
            return data;
        }

        public IList<CbDenomination> GetAllDenominationsByCountry(int countryId)
        {
            var data = _context.CbDenominations.Where(x => x.Country.Id == countryId).ToList();
            return data;
        }

        public IList<CbVariety> GetAllVarietiesByDenomination(int denominationId)
        {
            var data = _context.CbVarieties.Where(x => x.Denomination.Id == denominationId).ToList();
            return data;
        }

        public IList<CbType> GetAllTypesByVariety(int varietyId)
        {
            var data = _context.CbTypes.Where(x => x.Variety.Id == varietyId).ToList();
            return data;
        }
    }
}
