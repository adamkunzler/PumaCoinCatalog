using PumaCoinCatalog.Data;
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
    }
}
