namespace PumaCoinCatalog.Models
{
    public class CbChecklistValueSummary
    {
        public decimal CoinBullionValue { get; set; }
        public decimal CoinFaceValue { get; set; }

        public decimal FaceValueTotal { get; set; }
        public decimal BullionValueTotal { get; set; }
        public decimal EstimatedValueTotal { get; set; }
        public decimal CollectionValueTotal { get; set; }

        public int TotalCoinsInChecklist { get; set; }
        public int TotalCoinsCollected { get; set; }
        public int TotalCoinsPercentage { get; set; }
    }
}
