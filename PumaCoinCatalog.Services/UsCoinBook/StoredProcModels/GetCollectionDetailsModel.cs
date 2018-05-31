namespace PumaCoinCatalog.Services.UsCoinBook.StoredProcModels
{
    public class GetCollectionDetailsModel
    {
        public int ChecklistId { get; set; }
        public string ChecklistTitle { get; set; }
        public int TypeId { get; set; }
        public decimal TypeMeltValue { get; set; }
        public short BeginDate { get; set; }
        public short EndDate { get; set; }
        public string ObverseImageUri { get; set; }
        public string ReverseImageUri { get; set; }
        public int DenominationId { get; set; }
        public string DenominationTitle { get; set; }
        public decimal DenominationFaceValue { get; set; }

        public decimal CollectionValueTotal { get; set; }
        public int TotalCoinsInChecklist { get; set; }
        public int TotalCoinsCollected { get; set; }
        public int TotalCoinsPercentage { get; set; }
    }
}
