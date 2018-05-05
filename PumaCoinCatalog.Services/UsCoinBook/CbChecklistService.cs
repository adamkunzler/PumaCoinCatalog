using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
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

        public CbChecklistService()
        {
            _context = new DataContext();
        }

        public CbChecklistService(DataContext context)
        {
            _context = context;
        }

        public IList<CbChecklist> GetChecklistByCollection(int collectionId)
        {
            var data = _context.CbChecklists.Where(x => x.Collection.Id == collectionId).ToList();
            return data;
        }
    }
}
