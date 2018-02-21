using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class RawCoinCategory
    {
        public RawCoinCategory()
        {
            Id = Guid.NewGuid();
            CoinTypes = new List<RawCoinType>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }        
        public string ImageUrl { get; set; }

        public IList<RawCoinType> CoinTypes { get; set; }

        public override string ToString()
        {
            return $"Category - {Title}";
        }
    }
}
