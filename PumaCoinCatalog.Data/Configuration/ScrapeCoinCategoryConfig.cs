using PumaCoinCatalog.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class ScrapeCoinCategoryConfig : EntityTypeConfiguration<ScrapeCoinCategory>
    {
        public ScrapeCoinCategoryConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).HasMaxLength(64).IsRequired();            
            
            HasRequired(x => x.CoinCollection);
        }
    }
}
