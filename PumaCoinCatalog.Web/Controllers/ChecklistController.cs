using PumaCoinCatalog.Services;
using PumaCoinCatalog.Web.Infrastructure;
using PumaCoinCatalog.Web.Infrastructure.Mappers;
using PumaCoinCatalog.Web.Models.Checklist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Controllers
{
    public class ChecklistController : Controller
    {
        private readonly ChecklistService _checklistService;
        private readonly CoinDataService _coinDataService;

        public ChecklistController()
        {
            _checklistService = new ChecklistService();
            _coinDataService = new CoinDataService();
        }

        #region Actions

        public ActionResult Index(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Checklist Id is empty");

            var checklist = _checklistService.GetChecklist(id);
            if (checklist == null) throw new Exception("checklist not found");
            
            var model = ChecklistMapper.MapToChecklistModel(checklist);
            model.ChecklistInfoModel = GetChecklistInfoModel(model);
            
            return View(model);
        }

        public ActionResult NewChecklist()
        {
            var model = GetNewChecklistModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult NewChecklist(string title, Guid selectedCollectionId, Guid selectedCategoryId, Guid selectedTypeId)
        {
            var newChecklistId = _checklistService.CreateNewChecklist(title, selectedCollectionId, selectedCategoryId, selectedTypeId);
            return RedirectToAction("Index", "Checklist", new { id = newChecklistId });
        }

        public ActionResult AllChecklists()
        {
            var checklists = _checklistService.GetAllChecklists();
            var model = ChecklistMapper.MapToAllChecklistsModel(checklists);
            return View(model);
        }

        public ActionResult ChecklistTotals()
        {
            var checklists = _checklistService.GetAllChecklists();

            var model = new List<ChecklistModel>();
            foreach(var checklist in checklists)
            {
                var checklistModel = ChecklistMapper.MapToChecklistModel(checklist);                
                checklistModel.ChecklistInfoModel = GetChecklistInfoModel(checklistModel);
                model.Add(checklistModel);
            }

            return View(model);
        }

        #endregion Actions

        #region Ajax

        [HttpPost]
        public ActionResult DeleteChecklist(Guid checklistId)
        {
            _checklistService.DeleteChecklist(checklistId);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetChecklistTotals(Guid checklistId)
        {
            if (checklistId == Guid.Empty) throw new ArgumentException("Checklist Id is empty");

            var checklist = _checklistService.GetChecklist(checklistId);
            if (checklist == null) throw new Exception("checklist not found");

            var checklistModel = ChecklistMapper.MapToChecklistModel(checklist);
            var model = GetChecklistInfoModel(checklistModel);

            return PartialView("_checklistInfo", model);
        }

        [HttpPost]
        public ActionResult UpdateChecklistCoinEstimatedValue(Guid checklistCoinId, decimal value)
        {
            _checklistService.UpdateChecklistCoinEstimatedValue(checklistCoinId, value);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateChecklistCoinGrade(Guid checklistCoinId, short value)
        {
            _checklistService.UpdateChecklistCoinGrade(checklistCoinId, value);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateChecklistCoinRemoveFromChecklist(Guid checklistCoinId)
        {
            _checklistService.UpdateChecklistCoinInCollection(checklistCoinId, false);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateChecklistCoinAddToChecklist(Guid checklistCoinId)
        {
            _checklistService.UpdateChecklistCoinInCollection(checklistCoinId, true);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCoinCategoriesDropDown(Guid collectionId)
        {
            var model = GetCoinCategoriesDropDown(collectionId);
            return PartialView("_ddlCoinCategories", model);
        }

        [HttpPost]
        public ActionResult GetCoinTypesDropDown(Guid categoryId)
        {
            var model = GetCoinTypeDropDownModel(categoryId);            
            return PartialView("_ddlCoinTypes", model);
        }

        [HttpPost]
        public ActionResult UpdateCoinTypeBullionValue(Guid checklistId, decimal value)
        {
            var checklist = _checklistService.GetChecklist(checklistId);
            _coinDataService.UpdateCoinTypeBullionValue(checklist.CoinType.Id, value);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion Ajax

        #region Private Methods - Models

        private ChecklistInfoModel GetChecklistInfoModel(ChecklistModel checklist)
        {
            var calculator = new ChecklistCalculator(checklist);

            var model = new ChecklistInfoModel();
            model.CoinBullionValue = checklist.BullionValue;
            model.CoinFaceValue = checklist.FaceValue;
            model.TotalCoinsCollected = calculator.GetNumberOfCoinsCollectedInChecklist();
            model.TotalCoinsInChecklist = calculator.GetNumberOfCoinsInChecklist();
            model.TotalCoinsPercentage = (int)((model.TotalCoinsCollected / (decimal)model.TotalCoinsInChecklist) * 100);
            model.FaceValueTotal = calculator.CalculateFaceValueTotal();
            model.BullionValueTotal = calculator.CalculateBullionValueTotal();
            model.EstimatedValueTotal = calculator.CalculateEstimatedValueTotal();
            model.CollectionValueTotal = calculator.CalculateCollectionValueTotal();

            return model;
        }

        private NewChecklistModel GetNewChecklistModel()
        {
            var model = new NewChecklistModel();

            // retrieve collections            
            model.CoinCollectionDropDownModel = GetCoinCollectionDropDownModel();

            // retrieve categories
            var collectionId = Guid.Parse(model.CoinCollectionDropDownModel.SelectedCollectionId);
            model.CoinCategoryDropDownModel = GetCoinCategoryDropDownModel(collectionId);

            // retrieve types
            var categoryId = Guid.Parse(model.CoinCategoryDropDownModel.SelectedCategoryId);
            model.CoinTypeDropDownModel = GetCoinTypeDropDownModel(categoryId);

            return model;
        }

        private CoinCollectionDropDownModel GetCoinCollectionDropDownModel()
        {
            var collections = _coinDataService.GetAllCollections();
            if (!collections.Any()) throw new Exception("no collections in database");
            
            var model = new CoinCollectionDropDownModel();
            model.Collections = DropDownListHelper.ToSelectListItems(collections);
            model.SelectedCollectionId = model.Collections.First().Value;

            return model;
        }

        private CoinCategoryDropDownModel GetCoinCategoryDropDownModel(Guid collectionId)
        {
            var categories = _coinDataService.GetAllCategoriesByCollection(collectionId);
            if (!categories.Any()) throw new Exception("no categories for collection in database");

            var model = new CoinCategoryDropDownModel();
            model.Categories = DropDownListHelper.ToSelectListItems(categories);
            model.SelectedCategoryId = model.Categories.First().Value;

            return model;
        }

        private CoinTypeDropDownModel GetCoinTypeDropDownModel(Guid categoryId)
        {            
            var types = _coinDataService.GetAllTypesByCategory(categoryId);
            if (!types.Any()) throw new Exception("no types for category in database");

            var model = new CoinTypeDropDownModel();
            model.Types = DropDownListHelper.ToSelectListItems(types);
            model.SelectedTypeId = model.Types.First().Value;

            return model;
        }

        #endregion Private Methods - Models
    }
}