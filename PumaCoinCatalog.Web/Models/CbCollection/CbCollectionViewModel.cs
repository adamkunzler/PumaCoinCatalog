using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class CbCollectionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<CbChecklistViewModel> Checklists { get; set; }
    }
}