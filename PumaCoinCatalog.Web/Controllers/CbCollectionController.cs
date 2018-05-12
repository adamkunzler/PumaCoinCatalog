using PumaCoinCatalog.Services.UsCoinBook;
using PumaCoinCatalog.Web.Infrastructure.Mappers;
using PumaCoinCatalog.Web.Models.CbCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PumaCoinCatalog.Web.Infrastructure;
using System.IO;
using PumaCoinCatalog.UsCoinBook.Services;
using PumaCoinCatalog.Web.Models.CbCoinData;

namespace PumaCoinCatalog.Web.Controllers
{
    public class CbCollectionController : BaseController
    {
        private readonly CbCollectionService _collectionService;
        private readonly CbChecklistService _checklistService;
        private readonly CbGeneralService _generalService;
        private readonly CbCoinDataService _cbCoinDataService;

        public CbCollectionController()
        {
            _collectionService = new CbCollectionService();
            _checklistService = new CbChecklistService();            
            _generalService = new CbGeneralService();
            _cbCoinDataService = new CbCoinDataService();
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
                    Title = collection.Title,
                    Checklists = new List<CbCollectionChecklistViewModel>()
                };

                var checklists = _checklistService.GetChecklistByCollection(collectionId);
                foreach(var cbChecklist in checklists)
                {
                    var checklist = _checklistService.GetChecklist(cbChecklist.Id);
                    var type = _cbCoinDataService.GetType(checklist.Type.Id);
                    var valueSummary = _checklistService.GetChecklistValueSummary(checklist.Id);

                    var chkModel = new CbCollectionChecklistViewModel
                    {
                        Checklist = cbChecklist.Map(),
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
                        ValueSummary = CbChecklistController.GetValueSummaryViewModel(valueSummary)
                    };
                    model.Checklists.Add(chkModel);
                }

                model.NewChecklistViewModel = GetNewChecklistViewModel(collection.Id);

                return View(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateChecklist(int collectionId, string title, int typeId)
        {
            var result = _checklistService.CreateChecklist(title, collectionId, typeId);

            return Json(new
            {
                result = "success",
                checklistId = result.Id
            }, JsonRequestBehavior.AllowGet);
        }

        #region NewChecklist

        private NewChecklistViewModel GetNewChecklistViewModel(int collectionId)
        {
            var model = new NewChecklistViewModel
            {
                CollectionId = collectionId
            };
            
            // retrieve countries            
            model.CountryDropDownViewModel = GetChecklistCountryDropDownViewModel();

            // retrieve denominations
            var countryId = model.CountryDropDownViewModel.SelectedCountryId;
            model.DenominationDropDownViewModel = GetChecklistDenominationDropDownViewModel(countryId);

            // retrieve varieties
            var denominationId = model.DenominationDropDownViewModel.SelectedDenominationId;
            model.VarietyDropDownViewModel = GetChecklistVarietyDropDownViewModel(denominationId);

            // retrieve types
            var varietyId = model.VarietyDropDownViewModel.SelectedVarietyId;
            model.TypeDropDownViewModel = GetChecklistTypeDropDownViewModel(varietyId);

            return model;
        }

        private CbChecklistCountryDropDownModel GetChecklistCountryDropDownViewModel()
        {
            var collections = _generalService.GetAllCountries();
            if (!collections.Any()) throw new Exception("no countries in database");

            var model = new CbChecklistCountryDropDownModel();
            model.Countries = DropDownListHelper.ToSelectListItems(collections);
            model.SelectedCountryId = int.Parse(model.Countries.First().Value);

            return model;
        }

        private CbChecklistDenominationDropDownModel GetChecklistDenominationDropDownViewModel(int countryId)
        {
            var collections = _generalService.GetAllDenominationsByCountry(countryId);
            if (!collections.Any()) throw new Exception("no denominations for country in database");

            var model = new CbChecklistDenominationDropDownModel();
            model.Denominations = DropDownListHelper.ToSelectListItems(collections);
            model.SelectedDenominationId = int.Parse(model.Denominations.First().Value);

            return model;
        }

        private CbChecklistVarietyDropDownModel GetChecklistVarietyDropDownViewModel(int denominationId)
        {
            var categories = _generalService.GetAllVarietiesByDenomination(denominationId);
            if (!categories.Any()) throw new Exception("no varieties for denomination in database");

            var model = new CbChecklistVarietyDropDownModel();
            model.Varieties = DropDownListHelper.ToSelectListItems(categories);
            model.SelectedVarietyId = int.Parse(model.Varieties.First().Value);

            return model;
        }

        private CbChecklistTypeDropDownModel GetChecklistTypeDropDownViewModel(int varietyId)
        {
            var types = _generalService.GetAllTypesByVariety(varietyId);
            if (!types.Any()) throw new Exception("no types for variety in database");

            var model = new CbChecklistTypeDropDownModel();
            model.Types = DropDownListHelper.ToSelectListItems(types);
            model.SelectedTypeId = int.Parse(model.Types.First().Value);

            return model;
        }

        [HttpPost]
        public ActionResult GetCbDenominationsDropDown(int countryId)
        {
            var denominationsModel = GetChecklistDenominationDropDownViewModel(countryId);

            var denominationId = denominationsModel.SelectedDenominationId;
            var varietiesModel = GetChecklistVarietyDropDownViewModel(denominationId);

            var varietyId = varietiesModel.SelectedVarietyId;
            var typesModel = GetChecklistTypeDropDownViewModel(varietyId);

            var result = new
            {
                denominationsDropDownHtml = RenderPartialViewToString("_ddlCbChecklistDenominations", denominationsModel),
                varietiesDropDownHtml = RenderPartialViewToString("_ddlCbChecklistVarieties", varietiesModel),
                typesDropDownHtml = RenderPartialViewToString("_ddlCbChecklistTypes", typesModel)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCbVarietiesDropDown(int denominationId)
        {
            var varietiesModel = GetChecklistVarietyDropDownViewModel(denominationId);

            var varietyId = varietiesModel.SelectedVarietyId;
            var typesModel = GetChecklistTypeDropDownViewModel(varietyId);

            var result = new
            {
                varietiesDropDownHtml = RenderPartialViewToString("_ddlCbChecklistVarieties", varietiesModel),
                typesDropDownHtml = RenderPartialViewToString("_ddlCbChecklistTypes", typesModel)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCbTypesDropDown(int varietyId)
        {
            var model = GetChecklistTypeDropDownViewModel(varietyId);
            return PartialView("_ddlCbChecklistTypes", model);
        }

        #endregion NewChecklist

        
    }
}