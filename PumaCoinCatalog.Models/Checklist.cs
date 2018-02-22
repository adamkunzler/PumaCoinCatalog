using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Models
{
    public class Checklist
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime LastModified { get; set; }

        public virtual IList<ChecklistCoin> ChecklistCoins { get; set; }

        public virtual ScrapeCoinCollection CoinCollection { get; set; }
        public virtual ScrapeCoinCategory CoinCategory { get; set; }
        public virtual ScrapeCoinType CoinType { get; set; }        
    }
}
