using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class CbChecklistCoinConfig : EntityTypeConfiguration<CbChecklistCoin>
    {
        public CbChecklistCoinConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.InCollection).IsRequired();
            Property(x => x.ValueEstimate).IsRequired();
            Property(x => x.ShouldExclude).IsRequired();
            Property(x => x.Grade).IsRequired();

            HasRequired(x => x.Checklist);
            HasRequired(x => x.Coin);
        }
    }
}
