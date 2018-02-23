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
    }
}