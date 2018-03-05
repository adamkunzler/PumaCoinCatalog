using System;

namespace PumaCoinCatalog.Models
{
    public class ChecklistCoin
    {
        public Guid Id { get; set; }
        public bool InCollection { get; set; }
        public short? AdamGrade { get; set; }        
        public decimal? ValueEstimate { get; set; }
        public bool ShouldExclude { get; set; }

        public virtual Checklist Checklist { get; set; }

        public virtual ScrapeCoin Coin { get; set; }
    }
}
