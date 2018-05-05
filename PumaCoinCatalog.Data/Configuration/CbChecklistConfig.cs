using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class CbChecklistConfig : EntityTypeConfiguration<CbChecklist>
    {
        public CbChecklistConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Title).IsRequired().HasMaxLength(128);
            Property(x => x.LastModified).IsRequired();

            HasRequired(x => x.Type);
            HasRequired(x => x.Collection);            
        }
    }
}
