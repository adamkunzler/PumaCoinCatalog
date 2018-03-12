using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Models.UsaCoinBook
{
    public class CbGradeValue
    {
        public int Id { get; set; }
        public decimal Value { get; set; }

        public virtual CbGrade Grade { get; set; }
        public virtual CbCoin Coin { get; set; }
    }
}
