using PumaCoinCatalog.Web.Models.CbCoinData;
using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class CbCollectionDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<CbCollectionChecklistViewModel> Checklists { get; set; }
        public NewChecklistViewModel NewChecklistViewModel { get; set; }        
    }    

    public class CbCollectionChecklistViewModel
    {
        public int ChecklistId { get; set; }
        public string ChecklistTitle { get; set; }
        public int TypeId { get; set; }
        public int BeginDate { get; set; }
        public int EndDate { get; set; }
        public CbCoinImageViewModel ImageViewModel { get; set; }        
        public int DenominationId { get; set; }
        public string DenominationTitle { get; set; }

        //public CbChecklistViewModel Checklist { get; set; }
        //public CbChecklistTypeDetailsViewModel TypeDetails { get; set; }
        public CbChecklistValueSummaryViewModel ValueSummary { get; set; }
    }
}