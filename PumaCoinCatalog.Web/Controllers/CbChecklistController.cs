using PumaCoinCatalog.Models.UsaCoinBook;
using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using PumaCoinCatalog.Services.UsCoinBook;
using PumaCoinCatalog.UsCoinBook.Services;
using PumaCoinCatalog.Web.Infrastructure.Mappers;
using PumaCoinCatalog.Web.Models.CbCoinData;
using PumaCoinCatalog.Web.Models.CbCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Controllers
{
    public class CbChecklistController : Controller
    {
        public readonly CbChecklistService _checklistService;
        private readonly CbCoinDataService _cbCoinDataService;

        public CbChecklistController()
        {
            _checklistService = new CbChecklistService();
            _cbCoinDataService = new CbCoinDataService();
        }

        // GET: CbChecklist
        public ActionResult Index(int checklistId)
        {
            var checklist = _checklistService.GetChecklist(checklistId);
            var type = _cbCoinDataService.GetType(checklist.Type.Id);

            var model = new CbChecklistIndexViewModel
            {
                Id = checklist.Id,
                Title = checklist.Title,
                LastModified = checklist.LastModified,
                Collection = new CbCollectionViewModel {
                    Id = checklist.Collection.Id,
                    Title = checklist.Collection.Title
                },
                Type = type.Map(),
                Coins = checklist.Coins.Map()
            };

            return View(model);
        }

        #region Ajax

        [HttpPost]
        public ActionResult DeleteChecklist(int checklistId)
        {
            _checklistService.DeleteChecklist(checklistId);
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddCoinToChecklist(int checklistCoinId)
        {
            _checklistService.AddCoinToChecklist(checklistCoinId);
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveCoinFromChecklist(int checklistCoinId)
        {
            _checklistService.RemoveCoinFromChecklist(checklistCoinId);
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateChecklistCoinEstimatedValue(int checklistCoinId, decimal value)
        {
            _checklistService.UpdateChecklistCoinEstimatedValue(checklistCoinId, value);
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateChecklistCoinGrade(int checklistCoinId, CbGrade grade)
        {
            _checklistService.UpdateChecklistCoinGrade(checklistCoinId, grade);
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateChecklistCoinAddExclude(int checklistCoinId)
        {
            _checklistService.SetChecklistCoinExlude(checklistCoinId, true);
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateChecklistCoinRemoveExclude(int checklistCoinId)
        {
            _checklistService.SetChecklistCoinExlude(checklistCoinId, false);
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        #endregion Ajax
    }
}