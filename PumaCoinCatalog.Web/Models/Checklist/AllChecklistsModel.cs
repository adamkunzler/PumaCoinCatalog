using System;

namespace PumaCoinCatalog.Web.Models.Checklist
{
    public class AllChecklistsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CoinCollection { get; set; }
        public string CoinCategory { get; set; }
        public string CoinType { get; set; }
        public string Base64Image { get; set; }

        public string DisplayCoinType => $"{CoinCollection} | {CoinCategory} | {CoinType}";
    }
}