using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CbCoinData
{
    public class CbTypeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public short BeginDate { get; set; }
        public short EndDate { get; set; }
        public string MetalComposition { get; set; }
        public float Diameter { get; set; }
        public float Mass { get; set; }
        public decimal MeltValue { get; set; }
        public string ObverseImageUri { get; set; }
        public string ReverseImageUri { get; set; }
        public string SourceUri { get; set; }

        public IList<CbCoinViewModel> Coins { get; set; }

        public CbCoinImageViewModel ImageViewModel { get; set; }

        // for breadcrumb
        public string CountryTitle { get; set; }
        public int DenominationId { get; set; }
        public string DenominationTitle { get; set; }
        public int VarietyId { get; set; }
        public string VarietyTitle { get; set; }
    }
}