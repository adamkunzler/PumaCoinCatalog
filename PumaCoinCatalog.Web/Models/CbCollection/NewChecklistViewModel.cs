using System.Collections.Generic;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class NewChecklistViewModel
    {
        public NewChecklistViewModel()
        {
        }

        public int CollectionId { get; set; }
        public string Title { get; set; }
        public CbChecklistCountryDropDownModel CountryDropDownViewModel { get; set; }
        public CbChecklistDenominationDropDownModel DenominationDropDownViewModel { get; set; }
        public CbChecklistVarietyDropDownModel VarietyDropDownViewModel { get; set; }
        public CbChecklistTypeDropDownModel TypeDropDownViewModel { get; set; }
    }

    public class CbChecklistCountryDropDownModel
    {
        public int SelectedCountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
    }

    public class CbChecklistDenominationDropDownModel
    {
        public int SelectedDenominationId { get; set; }
        public IEnumerable<SelectListItem> Denominations { get; set; }
    }

    public class CbChecklistVarietyDropDownModel
    {
        public int SelectedVarietyId { get; set; }
        public IEnumerable<SelectListItem> Varieties { get; set; }
    }

    public class CbChecklistTypeDropDownModel
    {
        public int SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
    }
}