using PumaCoinCatalog.Models;
using PumaCoinCatalog.Models.UsaCoinBook;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Infrastructure
{
    public static class DropDownListHelper
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(IList<ScrapeCoinCollection> items)
        {            
            var selectList = items.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            return selectList;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(IList<ScrapeCoinCategory> items)
        {
            var selectList = items.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            return selectList;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(IList<ScrapeCoinType> items)
        {
            var selectList = items.Select(x => new SelectListItem { Text = x.Title + " - " + x.Details, Value = x.Id.ToString() });
            return selectList;
        }

        // ---------------------------------------------------------------------

        public static IEnumerable<SelectListItem> ToSelectListItems(IList<CbCountry> items)
        {
            var selectList = items.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            return selectList;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(IList<CbDenomination> items)
        {
            var selectList = items.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            return selectList;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(IList<CbVariety> items)
        {
            var selectList = items.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            return selectList;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(IList<CbType> items)
        {
            var selectList = items.Select(x => new SelectListItem { Text = $"{x.Title} ({x.BeginDate} - {x.EndDate})", Value = x.Id.ToString() });
            return selectList;
        }
    }
}