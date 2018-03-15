using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CbCoinData
{
    public class CbCountryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUri { get; set; }

        public IList<CbDenominationViewModel> Denominations { get; set; }
    }
}