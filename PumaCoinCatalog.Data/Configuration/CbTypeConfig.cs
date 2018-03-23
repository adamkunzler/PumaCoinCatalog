using PumaCoinCatalog.Models.UsaCoinBook;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class CbTypeConfig : EntityTypeConfiguration<CbType>
    {
        public CbTypeConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).IsRequired().HasMaxLength(128);
            Property(x => x.BeginDate).IsRequired();
            Property(x => x.EndDate).IsRequired();
            Property(x => x.MetalComposition).IsRequired().HasMaxLength(128);
            Property(x => x.Diameter).IsRequired();
            Property(x => x.Mass).IsRequired();
            Property(x => x.MeltValue).IsRequired().HasPrecision(18,4);
            Property(x => x.ObverseImageUri).IsOptional();
            Property(x => x.ReverseImageUri).IsOptional();

            HasRequired(x => x.Variety);
        }
    }
}
