using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models.UsaCoinBook.Checklists
{
    public class CbChecklist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime LastModified { get; set; }

        public virtual CbCollection Collection { get; set; }
        public virtual CbType Type { get; set; }
        public virtual IList<CbChecklistCoin> Coins { get; set; }
    }
}
