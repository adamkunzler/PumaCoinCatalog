using System;

namespace PumaCoinCatalog.Web.Models.CoinData
{
    public class CoinModel
    {
        public Guid Id { get; set; }
        public string Year { get; set; }
        public string Variety { get; set; }
        public int Mintage { get; set; }
        public string KmNumber { get; set; }
        public int NumisMediaId { get; set; }
        public int NgcId { get; set; }
        public int PcgsId { get; set; }
        public int SortOrder { get; set; }
    }
}