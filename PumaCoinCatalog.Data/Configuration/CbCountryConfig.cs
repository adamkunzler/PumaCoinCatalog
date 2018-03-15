using PumaCoinCatalog.Models.UsaCoinBook;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class CbCountryConfig : EntityTypeConfiguration<CbCountry>
    {
        public CbCountryConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).IsRequired().HasMaxLength(128);
            Property(x => x.ImageUri).IsOptional();            
        }
    }
}
