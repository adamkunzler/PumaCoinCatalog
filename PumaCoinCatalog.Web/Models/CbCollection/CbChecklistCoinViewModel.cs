using PumaCoinCatalog.Models.UsaCoinBook;
using PumaCoinCatalog.Web.Models.CbCoinData;

namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class CbChecklistCoinViewModel
    {
        public int Id { get; set; }
        public bool InCollection { get; set; }
        public CbGrade Grade { get; set; }
        public decimal ValueEstimate { get; set; }
        public int Quantity { get; set; }
        public bool ShouldExclude { get; set; }

        public CbChecklistViewModel Checklist { get; set; }
        public CbCoinViewModel Coin { get; set; }
    }
}