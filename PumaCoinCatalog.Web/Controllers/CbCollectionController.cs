using PumaCoinCatalog.Services.UsCoinBook;
using PumaCoinCatalog.Web.Infrastructure.Mappers;
using PumaCoinCatalog.Web.Models.CbCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Controllers
{
    public class CbCollectionController : Controller
    {
        private readonly CbCollectionService _collectionService;

        public CbCollectionController()
        {
            _collectionService = new CbCollectionService();
        }

        // GET: CbCollection
        public ActionResult Index()
        {
            var model = new CbCollectionIndexViewModel();

            var collections = _collectionService.GetAllCollections();
            model.Collections = collections.Map();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCollection(string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title)) return new HttpStatusCodeResult(500, "Must specify a title.");

                var collection = _collectionService.CreateCollection(title);
                if (collection == null) return new HttpStatusCodeResult(500);

                return Json(new { newCollectionId = collection.Id }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }
    }
}