using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class CbCollectionDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<CbChecklistViewModel> Checklists { get; set; }
    }
}