using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class ScrapeCoinCollection
    {
        public ScrapeCoinCollection()
        {
            Id = Guid.NewGuid();
            CoinCategories = new List<ScrapeCoinCategory>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }

        public IList<ScrapeCoinCategory> CoinCategories { get; set; }

        public override string ToString()
        {
            return $"Coin Collection - {Title}";
        }
    }
}
