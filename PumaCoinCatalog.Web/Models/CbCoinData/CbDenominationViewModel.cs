using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CbCoinData
{
    public class CbDenominationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal FaceValue { get; set; }
        public string ImageUri { get; set; }

        public IList<CbVarietyViewModel> Varieties { get; set; }

        // for breadcrumb        
        public string CountryTitle { get; set; }
    }
}