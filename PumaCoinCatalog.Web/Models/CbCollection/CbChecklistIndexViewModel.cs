using PumaCoinCatalog.Web.Models.CbCoinData;
using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class CbChecklistIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime LastModified { get; set; }

        public CbCollectionViewModel Collection { get; set; }        
        public CbChecklistTypeDetailsViewModel TypeDetails { get; set; }
        public IList<CbChecklistCoinViewModel> Coins { get; set; }
    }
}