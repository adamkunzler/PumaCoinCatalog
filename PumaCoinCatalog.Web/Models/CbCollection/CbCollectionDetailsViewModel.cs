using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class CbCollectionDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<CbChecklistViewModel> Checklists { get; set; }
        public NewChecklistViewModel NewChecklistViewModel { get; set; }
    }    
}