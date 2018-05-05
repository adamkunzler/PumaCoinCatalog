using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class CbCollectionConfig : EntityTypeConfiguration<CbCollection>
    {
        public CbCollectionConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Title).IsRequired().HasMaxLength(128);            
        }
    }
}
