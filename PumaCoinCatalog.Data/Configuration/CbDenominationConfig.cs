using PumaCoinCatalog.Models.UsaCoinBook;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class CbDenominationConfig : EntityTypeConfiguration<CbDenomination>
    {
        public CbDenominationConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).IsRequired().HasMaxLength(128);
            Property(x => x.FaceValue).IsRequired();
            Property(x => x.SourceUri).IsRequired().HasMaxLength(256);
            Property(x => x.ImageUri).IsOptional();

            HasRequired(x => x.Country);            
        }
    }
}
