using PumaCoinCatalog.Web.Models.CoinData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PumaCoinCatalog.Web.Models.Checklist
{
    public class ChecklistModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CoinTypeDisplay { get; set; }
        public string Base64Image { get; set; }
        public decimal FaceValue { get; set; }
        
        public IList<ChecklistCoinModel> ChecklistCoins { get; set; }
    }

    public class ChecklistCoinModel
    {
        public Guid Id { get; set; }        
        public bool InCollection { get; set; }
        public short? AdamGrade { get; set; }
        public decimal? ValueEstimate { get; set; }
        public CoinModel CoinModel { get; set; }
    }
}