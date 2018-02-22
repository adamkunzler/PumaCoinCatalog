using PumaCoinCatalog.Models;
using PumaCoinCatalog.Web.Models.CoinData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PumaCoinCatalog.Web.Infrastructure.Mappers
{
    public static class ScrapeCoinMapper
    {
        public static CoinCollectionModel Map(this ScrapeCoinCollection value)
        {
            var model = new CoinCollectionModel();

            model.Id = value.Id;
            model.Title = value.Title;
            model.Categories = new List<CoinCategoryModel>();

            foreach (var category in value.CoinCategories)
            {
                var catModel = new CoinCategoryModel
                {
                    Id = category.Id,
                    Title = category.Title,
                    SortOrder = category.SortOrder,
                    Base64Image = category.Base64Image,
                    Types = new List<CoinTypeModel>()
                };

                model.Categories.Add(catModel);
            }

            model.Categories = model.Categories.OrderBy(x => x.SortOrder).ToList();

            return model;
        }

        public static CoinCategoryModel Map(this ScrapeCoinCategory value)
        {
            var model = new CoinCategoryModel
            {
                Id = value.Id,
                Title = value.Title,
                SortOrder = value.SortOrder,
                Base64Image = value.Base64Image,
                Types = new List<CoinTypeModel>(),
                CollectionTitle = value.CoinCollection.Title,
                CollectionId = value.CoinCollection.Id
            };

            foreach (var type in value.CoinTypes)
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
                
                model.Types.Add(typeModel);
            }

            model.Types = model.Types.OrderBy(x => x.SortOrder).ToList();

            return model;
        }

        public static CoinTypeModel Map(this ScrapeCoinType value)
        {
            var model = new CoinTypeModel
            {
                Id = value.Id,
                Title = value.Title,
                Details = value.Details,
                SortOrder = value.SortOrder,
                Base64Image = value.Base64Image,
                Coins = new List<CoinModel>(),
                CollectionTitle = value.CoinCategory.CoinCollection.Title,
                CollectionId = value.CoinCategory.CoinCollection.Id,
                CategoryTitle = value.CoinCategory.Title,
                CategoryId = value.CoinCategory.Id
            };

            foreach (var coin in value.Coins)
            {
                var coinModel = new CoinModel
                {
                    Id = coin.Id,
                    Year = coin.Year,
                    Variety = coin.Variety,
                    Mintage = coin.Mintage,
                    KmNumber = coin.KmNumber,
                    NumisMediaId = coin.NumisMediaId,
                    NgcId = coin.NgcId,
                    PcgsId = coin.PcgsId,
                    SortOrder = coin.SortOrder
                };

                model.Coins.Add(coinModel);
            }

            model.Coins = model.Coins.OrderBy(x => x.SortOrder).ToList();

            return model;
        }
    }
}