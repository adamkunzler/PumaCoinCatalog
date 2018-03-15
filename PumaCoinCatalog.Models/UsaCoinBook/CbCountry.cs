using System.Collections.Generic;

namespace PumaCoinCatalog.Models.UsaCoinBook
{
    public class CbCountry
    {
        public CbCountry()
        {
            Denominations = new List<CbDenomination>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUri { get; set; }

        public virtual IList<CbDenomination> Denominations { get; set; }
    }
}
