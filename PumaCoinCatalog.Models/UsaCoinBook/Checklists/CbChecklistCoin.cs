namespace PumaCoinCatalog.Models.UsaCoinBook.Checklists
{
    public class CbChecklistCoin
    {
        public int Id { get; set; }
        public bool InCollection { get; set; }
        public CbGrade Grade { get; set; }
        public decimal ValueEstimate { get; set; }
        public bool ShouldExclude { get; set; }

        public virtual CbChecklist Checklist { get; set; }
        public virtual CbCoin Coin { get; set; }
    }
}
