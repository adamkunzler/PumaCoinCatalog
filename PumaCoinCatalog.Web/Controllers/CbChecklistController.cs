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
                Coins = PrepareCoinsModel(checklist.Coins)
            };

            return View(model);
        }

        private IList<CbChecklistCoinViewModel> PrepareCoinsModel(IList<CbChecklistCoin> coins)
        {
            return new List<CbChecklistCoinViewModel>();
        }
    }
}