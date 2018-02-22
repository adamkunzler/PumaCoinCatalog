using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Controllers
{
    public class ChecklistController : Controller
    {
        public ActionResult Index(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Checklist Id is empty");

            return View();
        }

        public ActionResult NewChecklist()
        {
            return View();
        }

        public ActionResult AllChecklists()
        {
            return View();
        }
    }
}