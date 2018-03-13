using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Models.UsaCoinBook
{
    public class CbCoin
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string MintMark { get; set; }
        public string Details { get; set; }
        public long Mintage { get; set; }
        
        public virtual CbVariety Variety { get; set; }
        public virtual IList<CbGradeValue> GradeValues { get; set; }
    }
}
