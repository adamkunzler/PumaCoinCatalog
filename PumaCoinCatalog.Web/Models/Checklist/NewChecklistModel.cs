using System.Collections.Generic;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Models.Checklist
{
    public class NewChecklistModel
    {
        public NewChecklistModel()
        {
        }

        public string Title { get; set; } 
        public CoinCollectionDropDownModel CoinCollectionDropDownModel { get; set; }
        public CoinCategoryDropDownModel CoinCategoryDropDownModel { get; set; }
        public CoinTypeDropDownModel CoinTypeDropDownModel { get; set; }
    }

    public class CoinCollectionDropDownModel
    {
        public string SelectedCollectionId { get; set; }
        public IEnumerable<SelectListItem> Collections { get; set; }
    }

    public class CoinCategoryDropDownModel
    {
        public string SelectedCategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }

    public class CoinTypeDropDownModel
    {
        public string SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
    }
}