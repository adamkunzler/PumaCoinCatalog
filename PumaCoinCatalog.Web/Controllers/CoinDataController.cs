using PumaCoinCatalog.Services;
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
            var collection = _coinDataService.GetUsCoinCollection();

            // map to model
            #region Map to Model
            var model = new IndexModel();
            model.Collection = new CoinCollectionModel();
            model.Collection.Id = collection.Id;
            model.Collection.Title = collection.Title;
            model.Collection.Categories = new List<CoinCategoryModel>();
            foreach(var category in collection.CoinCategories)
            {
                var catModel = new CoinCategoryModel
                {
                    Id = category.Id,
                    Title = category.Title,
                    SortOrder = category.SortOrder,
                    Base64Image = category.Base64Image,
                    Types = new List<CoinTypeModel>()
                };

                foreach(var type in category.CoinTypes)
                {
                    var typeModel = new CoinTypeModel
                    {
                        Id = type.Id,
                        Title = type.Title,
                        Details = type.Details,
                        SortOrder = type.SortOrder,                        
                        Base64Image = type.Base64Image,
                        Coins = new List<CoinModel>()
                    };

                    foreach(var coin in type.Coins)
                    {
                        var coinModel = new CoinModel
                        {
                            Id = coin.Id,
                            Year = coin.Year,
                            Variety = coin.Variety,
                            Mintage = coin.Mintage,
                            SortOrder = coin.SortOrder
                        };

                        typeModel.Coins.Add(coinModel);
                    }

                    typeModel.Coins = typeModel.Coins.OrderBy(x => x.SortOrder).ToList();
                    catModel.Types.Add(typeModel);
                }

                catModel.Types = catModel.Types.OrderBy(x => x.SortOrder).ToList();
                model.Collection.Categories.Add(catModel);
            }

            model.Collection.Categories = model.Collection.Categories.OrderBy(x => x.SortOrder).ToList();
            #endregion Map to Model

            return View(model);
        }
    }
}