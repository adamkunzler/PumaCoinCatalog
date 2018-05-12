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
        public CbChecklistViewModel Checklist { get; set; }
        public CbChecklistTypeDetailsViewModel TypeDetails { get; set; }
        public CbChecklistValueSummaryViewModel ValueSummary { get; set; }
    }
}