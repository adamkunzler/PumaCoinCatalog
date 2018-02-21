using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class ScrapeCoinType
    {
        public ScrapeCoinType()
        {
            Id = Guid.NewGuid();
            Coins = new List<ScrapeCoin>();
        }

        public Guid Id { get; set; }
        public string Title {get;set; }
        public string Details { get; set; }
        public string ImageUri { get; set; }

        public virtual IList<ScrapeCoin> Coins { get; set; }
        public virtual ScrapeCoinCategory CoinCategory { get; set; }

        public override string ToString()
        {
            return $"Type - {Title} {Details}";
        }
    }
}
