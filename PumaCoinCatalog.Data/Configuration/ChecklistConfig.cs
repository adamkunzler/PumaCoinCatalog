using PumaCoinCatalog.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class ChecklistConfig : EntityTypeConfiguration<Checklist>
    {
        public ChecklistConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).HasMaxLength(128).IsRequired();
            Property(x => x.LastModified).IsRequired();

            HasRequired(x => x.CoinCollection);
            HasRequired(x => x.CoinCategory);
            HasRequired(x => x.CoinType);
        }        
    }
}
