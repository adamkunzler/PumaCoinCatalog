using PumaCoinCatalog.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class ChecklistCoinConfig : EntityTypeConfiguration<ChecklistCoin>
    {
        public ChecklistCoinConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(x => x.InCollection).IsRequired();
            Property(x => x.AdamGrade).IsOptional();
            Property(x => x.ValueEstimate).IsOptional();

            HasRequired(x => x.Checklist);
            HasRequired(x => x.Coin);
        }
    }
}
