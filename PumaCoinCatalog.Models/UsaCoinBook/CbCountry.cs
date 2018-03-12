using System.Collections.Generic;

namespace PumaCoinCatalog.Models.UsaCoinBook
{
    public class CbCountry
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual IList<CbDenomination> Denominations { get; set; }
    }
}
