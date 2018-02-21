using PumaCoinCatalog.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class ScrapeCoinConfig : EntityTypeConfiguration<ScrapeCoin>
    {
        public ScrapeCoinConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Year).HasMaxLength(8).IsRequired();
            Property(x => x.Variety).HasMaxLength(64);            
            Property(x => x.KmNumber).HasMaxLength(16);

            HasRequired(x => x.CoinType);
        }
    }
}
