using System.Collections.Generic;

namespace PumaCoinCatalog.Models.UsaCoinBook.Checklists
{
    public class CbCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IList<CbChecklist> Checklists { get; set; }
    }
}
