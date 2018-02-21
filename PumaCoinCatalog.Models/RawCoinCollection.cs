using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class RawCoinCollection
    {
        public RawCoinCollection()
        {
            Id = Guid.NewGuid();
            CoinCategories = new List<RawCoinCategory>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }

        public IList<RawCoinCategory> CoinCategories { get; set; }

        public override string ToString()
        {
            return $"Coin Collection - {Title}";
        }
    }
}
