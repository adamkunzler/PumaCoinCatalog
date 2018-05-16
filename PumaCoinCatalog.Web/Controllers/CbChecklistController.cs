using PumaCoinCatalog.Models;
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
            var valueSummary = _checklistService.GetChecklistValueSummary(checklist.Id);

            var model = new CbChecklistIndexViewModel
            {
                Id = checklist.Id,
                Title = checklist.Title,
                LastModified = checklist.LastModified,
                Collection = new CbCollectionViewModel {
                    Id = checklist.Collection.Id,
                    Title = checklist.Collection.Title
                },
                TypeDetails = new CbChecklistTypeDetailsViewModel
                {
                    DenominationId = type.Variety.Denomination.Id,
                    DenominationTitle = type.Variety.Denomination.Title,
                    VarietyId = type.Variety.Id,
                    VarietyTitle = type.Variety.Title,
                    TypeId = type.Id,                    
                    TypeTitle = type.Title,
                    ChecklistId = checklist.Id,
                    ChecklistTitle = checklist.Title,
                    BeginDate = type.BeginDate,
                    EndDate = type.EndDate,
                    MetalComposition = type.MetalComposition,
                    Diameter = type.Diameter,
                    Mass = type.Mass,
                    MeltValue = type.MeltValue,
                    SourceUri = type.Variety.SourceUri,
                    ImageViewModel = new CbCoinImageViewModel
                    {
                        ObverseImageUri = type.ObverseImageUri,
                        ReverseImageUri = type.ReverseImageUri,
                        Title = type.Title
                    }
                },
                ValueSummary = GetValueSummaryViewModel(valueSummary),
                Coins = checklist.Coins.Map()
            };

            return View(model);
        }

        #region Private Methods

        public static CbChecklistValueSummaryViewModel GetValueSummaryViewModel(CbChecklistValueSummary valueSummary)
        {
            var model = new CbChecklistValueSummaryViewModel
            {
                CoinBullionValue = valueSummary.CoinBullionValue,
                CoinFaceValue = valueSummary.CoinFaceValue,
                FaceValueTotal = valueSummary.FaceValueTotal,
                BullionValueTotal = valueSummary.BullionValueTotal,
                EstimatedValueTotal = valueSummary.EstimatedValueTotal,
                CollectionValueTotal = valueSummary.CollectionValueTotal,
                CoinCountBar = new CoinCountBarViewModel
                {
                    TotalCoinsInChecklist = valueSummary.TotalCoinsInChecklist,
                    TotalCoinsCollected = valueSummary.TotalCoinsCollected,
                    TotalCoinsPercentage = valueSummary.TotalCoinsPercentage
                }
            };
            return model;
        }

        #endregion Private Methods

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
        public ActionResult UpdateChecklistCoinQuanity(int checklistCoinId, int quantity)
        {
            _checklistService.UpdateChecklistCoinQuantity(checklistCoinId, quantity);
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

        [HttpPost]
        public ActionResult UpdateChecklistTitle(int checklistId, string title)
        {
            _checklistService.UpdateChecklistTitle(checklistId, title);
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RefreshChecklistValueSummary(int checklistId)
        {
            var valueSummary = _checklistService.GetChecklistValueSummary(checklistId);
            var model = GetValueSummaryViewModel(valueSummary);
            return PartialView("_valueSummary", model);
        }

        #endregion Ajax
    }
}