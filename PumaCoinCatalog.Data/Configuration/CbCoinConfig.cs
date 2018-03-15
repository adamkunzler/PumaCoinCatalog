using PumaCoinCatalog.Models.UsaCoinBook;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PumaCoinCatalog.Data.Configuration
{
    public class CbCoinConfig : EntityTypeConfiguration<CbCoin>
    {
        public CbCoinConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Year).IsRequired();
            Property(x => x.MintMark).IsRequired().HasMaxLength(8);
            Property(x => x.Details).IsRequired().HasMaxLength(128);
            Property(x => x.Mintage).IsRequired();

            HasRequired(x => x.Type);
        }
    }
}


// Country
//      Denomination
//          Variety
//              Type
//                  Coin
//