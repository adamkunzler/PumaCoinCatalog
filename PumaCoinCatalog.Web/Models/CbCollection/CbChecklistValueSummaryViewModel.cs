namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class CbChecklistValueSummaryViewModel
    {
        public decimal CoinBullionValue { get; set; }
        public decimal CoinFaceValue { get; set; }

        public decimal FaceValueTotal { get; set; }
        public decimal BullionValueTotal { get; set; }
        public decimal EstimatedValueTotal { get; set; }
        public decimal CollectionValueTotal { get; set; }

        public CoinCountBarViewModel CoinCountBar { get; set; }
    }
}