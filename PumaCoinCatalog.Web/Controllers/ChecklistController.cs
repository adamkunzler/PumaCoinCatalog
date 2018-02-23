using PumaCoinCatalog.Services;
using PumaCoinCatalog.Web.Infrastructure;
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

            return View();
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
            return View();
        }

        #endregion Actions

        #region Ajax

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

        #endregion Ajax

        #region Private Methods - Models

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