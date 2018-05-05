using PumaCoinCatalog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Services.UsCoinBook
{
    public class CbCollectionService
    {
        private readonly DataContext _context;

        public CbCollectionService()
        {
            _context = new DataContext();
        }

        public CbCollectionService(DataContext context)
        {
            _context = context;
        }
    }
}
