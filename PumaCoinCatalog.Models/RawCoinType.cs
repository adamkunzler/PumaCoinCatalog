using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class RawCoinType
    {
        public RawCoinType()
        {
            Id = Guid.NewGuid();
            Coins = new List<RawCoin>();
        }

        public Guid Id { get; set; }
        public string Title {get;set; }
        public string Details { get; set; }
        public string ImageUri { get; set; }

        public IList<RawCoin> Coins { get; set; }

        public override string ToString()
        {
            return $"Type - {Title} {Details}";
        }
    }
}
