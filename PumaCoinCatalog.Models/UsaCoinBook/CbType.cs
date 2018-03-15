using System.Collections.Generic;

namespace PumaCoinCatalog.Models.UsaCoinBook
{
    public class CbType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public short BeginDate { get; set; }
        public short EndDate { get; set; }        
        public string MetalComposition { get; set; }
        public float Diameter { get; set; }
        public float Mass { get; set; }
        public decimal MeltValue { get; set; }
        public string ObverseImageUri { get; set; }
        public string ReverseImageUri { get; set; }

        public virtual CbVariety Variety { get; set; }
        public virtual IList<CbCoin> Coins { get; set; }
    }
}
