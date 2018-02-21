using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class CoinCollection
    {
        public CoinCollection()
        {
            Id = Guid.NewGuid();
            CoinCategories = new List<CoinCategory>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }

        public IList<CoinCategory> CoinCategories { get; set; }

        public override string ToString()
        {
            return $"Coin Collection - {Title}";
        }
    }
}
