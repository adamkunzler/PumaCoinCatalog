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
        public static CoinCategoryModel MapCoinCategory(ScrapeCoinCategory category)
        {
            var model = new CoinCategoryModel
            {
                Id = category.Id,
                Title = category.Title,
                SortOrder = category.SortOrder,
                Base64Image = category.Base64Image,
                FaceValue = category.FaceValue / 1000m,
                Types = new List<CoinTypeModel>(),
                CollectionTitle = category.CoinCollection.Title,
                CollectionId = category.CoinCollection.Id
            };
            return model;
        }

        public static CoinTypeModel MapCoinType(ScrapeCoinType type)
        {
            var model = new CoinTypeModel
            {
                Id = type.Id,
                Title = type.Title,
                Details = type.Details,
                SortOrder = type.SortOrder,
                Base64Image = type.Base64Image,
                BullionValue = type.BullionValue,
                Coins = new List<CoinModel>(),
                CollectionTitle = type.CoinCategory.CoinCollection.Title,
                CollectionId = type.CoinCategory.CoinCollection.Id,
                CategoryTitle = type.CoinCategory.Title,
                CategoryId = type.CoinCategory.Id
            };
            return model;
        }

        public static CoinCollectionModel Map(this ScrapeCoinCollection value)
        {
            var model = new CoinCollectionModel();
            model.Id = value.Id;
            model.Title = value.Title;
            model.Categories = new List<CoinCategoryModel>();

            foreach (var category in value.CoinCategories)
            {
                var catModel = MapCoinCategory(category);

                model.Categories.Add(catModel);
            }

            model.Categories = model.Categories.OrderBy(x => x.SortOrder).ToList();

            return model;
        }

        public static CoinCategoryModel Map(this ScrapeCoinCategory value)
        {
            var model = MapCoinCategory(value);

            foreach (var type in value.CoinTypes)
            {
                var typeModel = MapCoinType(type);                
                model.Types.Add(typeModel);
            }

            model.Types = model.Types.OrderBy(x => x.SortOrder).ToList();

            return model;
        }

        public static CoinTypeModel Map(this ScrapeCoinType value)
        {
            var model = MapCoinType(value);

            foreach (var coin in value.Coins)
            {
                var coinModel = coin.Map();
                model.Coins.Add(coinModel);
            }

            model.Coins = model.Coins.OrderBy(x => x.SortOrder).ToList();

            return model;
        }

        public static CoinModel Map(this ScrapeCoin value)
        {            
            var coinModel = new CoinModel
            {
                Id = value.Id,
                Year = value.Year,
                Variety = value.Variety,
                Mintage = value.Mintage,
                KmNumber = value.KmNumber,
                NumisMediaId = value.NumisMediaId,
                NgcId = value.NgcId,
                PcgsId = value.PcgsId,
                SortOrder = value.SortOrder
            };

            return coinModel;
        }
    }
}