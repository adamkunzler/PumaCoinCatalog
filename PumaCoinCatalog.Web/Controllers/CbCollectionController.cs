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
        private readonly CbChecklistService _checklistService;

        public CbCollectionController()
        {
            _collectionService = new CbCollectionService();
            _checklistService = new CbChecklistService();
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

        public ActionResult Details(int collectionId)
        {
            try
            {
                if (collectionId <= 0) return new HttpStatusCodeResult(500, "Invalid collection Id");

                var collection = _collectionService.GetCollection(collectionId);
                var model = new CbCollectionDetailsViewModel
                {
                    Id = collection.Id,
                    Title = collection.Title
                };

                var checklists = _checklistService.GetChecklistByCollection(collectionId);
                model.Checklists = checklists.Map();

                return View(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }
    }
}