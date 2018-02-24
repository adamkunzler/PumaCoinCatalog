using PumaCoinCatalog.Models;
using PumaCoinCatalog.Web.Models.Checklist;
using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Infrastructure.Mappers
{
    public static class ChecklistMapper
    {
        public static AllChecklistsModel MapToAllChecklistsModel(Checklist checklist)
        {
            var model = new AllChecklistsModel
            {
                Id = checklist.Id,
                Title = checklist.Title,
                CoinCollection = checklist.CoinCollection.Title,
                CoinCategory = checklist.CoinCategory.Title,
                CoinType = checklist.CoinType.Title + " - " + checklist.CoinType.Details,
                Base64Image = checklist.CoinType.Base64Image
            };
            return model;
        }

        public static IList<AllChecklistsModel> MapToAllChecklistsModel(IList<Checklist> checklists)
        {
            var models = new List<AllChecklistsModel>();

            foreach (var checklist in checklists)
            {
                models.Add(MapToAllChecklistsModel(checklist));
            }

            return models;
        }

        public static ChecklistModel MapToChecklistModel(Checklist checklist)
        {
            var model = new ChecklistModel
            {
                Id = checklist.Id,
                Title = checklist.Title,
                CoinTypeDisplay = $"{checklist.CoinCollection.Title} | {checklist.CoinCategory.Title} | {checklist.CoinType.Title} - {checklist.CoinType.Details}",
                Base64Image = checklist.CoinType.Base64Image,
                FaceValue = checklist.CoinCategory.FaceValue / 1000m,
                BullionValue = checklist.CoinType.BullionValue,
                ChecklistCoins = new List<ChecklistCoinModel>()
            };
            
            foreach(var coin in checklist.ChecklistCoins)
            {
                var coinModel = new ChecklistCoinModel
                {
                    Id = coin.Id,                    
                    AdamGrade = coin.AdamGrade,
                    InCollection = coin.InCollection,
                    ValueEstimate = coin.ValueEstimate,
                    CoinModel = coin.Coin.Map()
                };

                model.ChecklistCoins.Add(coinModel);
            }

            return model;
        }

        public static IList<ChecklistExportModel> MapToChecklistExportModel(Checklist checklist)
        {
            var model = new List<ChecklistExportModel>();

            var coinType = $"{ checklist.CoinCollection.Title } | { checklist.CoinCategory.Title } | { checklist.CoinType.Title } - { checklist.CoinType.Details }";

            foreach (var coin in checklist.ChecklistCoins)
            {
                var exportModel = new ChecklistExportModel
                {
                    CoinType = coinType,
                    Year = coin.Coin.Year,
                    Variety = coin.Coin.Variety,
                    InCollection = coin.InCollection ? "X" : "",
                    Grade = coin.AdamGrade.HasValue ? GradeHelper.GetGradeFromNumber(coin.AdamGrade.Value) : "",
                    ValueEstimate = coin.ValueEstimate.ToString(),
                    Mintage = coin.Coin.Mintage.ToString(),
                    KmNumber = coin.Coin.KmNumber.ToString(),
                    NumisMediaId = coin.Coin.NumisMediaId.ToString(),
                    NgcId = coin.Coin.NgcId.ToString(),
                    PcgsId = coin.Coin.PcgsId.ToString()
                };
                model.Add(exportModel);
            }

            return model;
        }
    }
}