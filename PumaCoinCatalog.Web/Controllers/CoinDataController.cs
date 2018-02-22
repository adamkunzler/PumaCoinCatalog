using PumaCoinCatalog.Services;
using PumaCoinCatalog.Web.Infrastructure.Mappers;
using PumaCoinCatalog.Web.Models.CoinData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Controllers
{
    public class CoinDataController : Controller
    {
        private readonly CoinDataService _coinDataService;

        public CoinDataController()
        {
            _coinDataService = new CoinDataService();
        }

        // GET: ScrapeCoins
        public ActionResult Index()
        {
            var coinCollection = _coinDataService.GetUsCoinCollection();
            var model = coinCollection.Map();
            return View(model);
        }        

        public ActionResult CoinCategory(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("CoinCategory Id is empty");

            var category = _coinDataService.GetCoinCategory(id);
            var model = category.Map();
            return View(model);
        }

        public ActionResult CoinType(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("CoinType Id is empty");

            var type = _coinDataService.GetCoinType(id);
            var model = type.Map();
            return View(model);
        }
    }
}