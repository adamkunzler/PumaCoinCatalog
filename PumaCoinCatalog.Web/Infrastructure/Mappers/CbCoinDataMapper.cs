using PumaCoinCatalog.Models.UsaCoinBook;
using PumaCoinCatalog.Web.Models.CbCoinData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PumaCoinCatalog.Web.Infrastructure.Mappers
{
    public static class CbCoinDataMapper
    {
        public static CbCountryViewModel Map(this CbCountry data)
        {
            var model = new CbCountryViewModel();

            model.Id = data.Id;
            model.Title = data.Title;
            model.ImageUri = data.ImageUri;
            model.Denominations = Map(data.Denominations);

            return model;
        }

        public static IList<CbDenominationViewModel> Map(this IList<CbDenomination> data)
        {            
            var model = data.Select(x => x.Map()).ToList();
            return model;
        }

        public static CbDenominationViewModel Map(this CbDenomination data)
        {
            var model = new CbDenominationViewModel();

            model.Id = data.Id;
            model.Title = data.Title;
            model.FaceValue = data.FaceValue;
            model.ImageUri = data.ImageUri;
            model.Varieties = Map(data.Varieties);

            model.CountryTitle = data.Country.Title;

            return model;
        }

        public static IList<CbVarietyViewModel> Map(this IList<CbVariety> data)
        {
            var model = data.Select(x => x.Map()).ToList();
            return model;
        }

        public static CbVarietyViewModel Map(this CbVariety data)
        {
            var model = new CbVarietyViewModel();

            model.Id = data.Id;
            model.Title = data.Title;
            model.ObverseImageUri = data.ObverseImageUri;
            model.ReverseImageUri = data.ReverseImageUri;
            model.Types = Map(data.Types);

            model.CountryTitle = data.Denomination.Country.Title;
            model.DenominationId = data.Denomination.Id;
            model.DenominationTitle = data.Denomination.Title;


            return model;
        }

        public static IList<CbTypeViewModel> Map(this IList<CbType> data)
        {
            var model = data.Select(x => x.Map()).ToList();
            return model;
        }

        public static CbTypeViewModel Map(this CbType data)
        {
            var model = new CbTypeViewModel();

            model.Id = data.Id;
            model.Title = data.Title;
            model.BeginDate = data.BeginDate;
            model.EndDate = data.EndDate;
            model.MetalComposition = data.MetalComposition;
            model.Diameter = data.Diameter;
            model.Mass = data.Mass;
            model.MeltValue = data.MeltValue;
            model.ObverseImageUri = data.ObverseImageUri;
            model.ReverseImageUri = data.ReverseImageUri;
            model.Coins = Map(data.Coins);

            model.CountryTitle = data.Variety.Denomination.Country.Title;
            model.DenominationId = data.Variety.Denomination.Id;
            model.DenominationTitle = data.Variety.Denomination.Title;
            model.VarietyId = data.Variety.Id;
            model.VarietyTitle = data.Variety.Title;

            return model;
        }

        public static IList<CbCoinViewModel> Map(this IList<CbCoin> data)
        {
            var model = data.Select(x => x.Map()).ToList();
            return model;
        }

        public static CbCoinViewModel Map(this CbCoin data)
        {
            var model = new CbCoinViewModel();

            model.Id = data.Id;
            model.Year = data.Year;
            model.MintMark = data.MintMark;
            model.Details = data.Details;
            model.Mintage = data.Mintage;

            return model;
        }
    }
}