using System.Collections.Generic;

namespace PumaCoinCatalog.Models.UsaCoinBook
{
    public class CbDenomination
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal FaceValue { get; set; }
        public string SourceUri { get; set; }

        public virtual CbCountry Country { get; set; }
        public virtual IList<CbVariety> Varieties { get; set; }
    }
}
