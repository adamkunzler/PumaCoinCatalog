using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class CoinCategory
    {
        public CoinCategory()
        {
            Id = Guid.NewGuid();
            CoinTypes = new List<CoinType>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }        
        public string ImageUrl { get; set; }

        public IList<CoinType> CoinTypes { get; set; }

        public override string ToString()
        {
            return $"Category - {Title}";
        }
    }
}
