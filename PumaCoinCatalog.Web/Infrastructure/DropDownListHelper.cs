using PumaCoinCatalog.Models;
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
    }
}