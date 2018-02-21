using System;

namespace PumaCoinCatalog.Models
{
    public class RawCoin
    {
        public RawCoin()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Year { get; set; }
        public string Variety { get; set; }
        public int Mintage { get; set; }
        public string KmNumber { get; set; }
        public int NumisMediaId { get; set; }
        public int NgcId { get; set; }
        public int PcgsId { get; set; }

        public override string ToString()
        {
            return $"Coin - {Year} {Variety}";
        }
    }
}
