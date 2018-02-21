using PumaCoinCatalog.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class ScrapeCoinCollectionConfig : EntityTypeConfiguration<ScrapeCoinCollection>
    {
        public ScrapeCoinCollectionConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).HasMaxLength(64).IsRequired();            
        }
    }
}
