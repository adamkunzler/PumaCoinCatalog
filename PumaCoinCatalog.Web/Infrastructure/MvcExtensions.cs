using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace PumaCoinCatalog.Web.Infrastructure
{
    public static class MvcExtensions
    {
        //public static string RenderPartialToString(string viewName, object viewmodel)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //    {
        //        viewName = this.ControllerContext.RouteData.GetRequiredString("action");
        //    }

        //    ViewData.Model = viewmodel;

        //    using (var sw = new StringWriter())
        //    {
        //        ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
        //        var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
        //        viewResult.View.Render(viewContext, sw);
        //        viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
    }
}