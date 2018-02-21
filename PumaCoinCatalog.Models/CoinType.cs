using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class CoinType
    {
        public CoinType()
        {
            Id = Guid.NewGuid();
            Coins = new List<Coin>();
        }

        public Guid Id { get; set; }
        public string Title {get;set; }
        public string Details { get; set; }
        public string ImageUri { get; set; }

        public IList<Coin> Coins { get; set; }

        public override string ToString()
        {
            return $"Type - {Title} {Details}";
        }
    }
}
