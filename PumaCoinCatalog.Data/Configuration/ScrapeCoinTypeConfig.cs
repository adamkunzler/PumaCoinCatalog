using PumaCoinCatalog.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class ScrapeCoinTypeConfig : EntityTypeConfiguration<ScrapeCoinType>
    {
        public ScrapeCoinTypeConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).HasMaxLength(64).IsRequired();
            Property(x => x.Details).HasMaxLength(64);
            Property(x => x.ImageUri).HasMaxLength(1024);

            HasRequired(x => x.CoinCategory);
        }
    }
}
