﻿using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using PumaCoinCatalog.Web.Models.CbCollection;
using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Infrastructure.Mappers
{
    public static class CbCollectionMapper
    {
        public static IList<CbCollectionViewModel> Map(this IList<CbCollection> collections)
        {
            var models = new List<CbCollectionViewModel>();

            foreach(var c in collections)
            {
                models.Add(c.Map());
            }

            return models;
        }

        public static CbCollectionViewModel Map(this CbCollection collection)
        {
            var model = new CbCollectionViewModel
            {
                Id = collection.Id,
                Title = collection.Title,
                Checklists = collection.Checklists == null ? new List<CbChecklistViewModel>() : collection.Checklists.Map()                
            };
            
            return model;
        }

        public static IList<CbChecklistViewModel> Map(this IList<CbChecklist> checklists)
        {
            var models = new List<CbChecklistViewModel>();

            foreach (var c in checklists)
            {
                models.Add(c.Map());
            }

            return models;
        }

        public static CbChecklistViewModel Map(this CbChecklist checklist)
        {
            var model = new CbChecklistViewModel
            {
                Id = checklist.Id,
                Title = checklist.Title,
                LastModified = checklist.LastModified,
                Type = checklist.Type.Map(),
                Coins = checklist.Coins.Map()        
            };
            
            return model;
        }

        public static IList<CbChecklistCoinViewModel> Map(this IList<CbChecklistCoin> checklistCoins)
        {
            var models = new List<CbChecklistCoinViewModel>();

            foreach (var c in checklistCoins)
            {
                models.Add(c.Map());
            }

            return models;
        }

        public static CbChecklistCoinViewModel Map(this CbChecklistCoin checklistCoin)
        {
            var model = new CbChecklistCoinViewModel
            {
                Id = checklistCoin.Id,
                InCollection = checklistCoin.InCollection,
                ShouldExclude = checklistCoin.ShouldExclude,
                ValueEstimate = checklistCoin.ValueEstimate,
                Grade = checklistCoin.Grade,
                Coin = checklistCoin.Coin.Map()
            };

            return model;
        }
    }
}