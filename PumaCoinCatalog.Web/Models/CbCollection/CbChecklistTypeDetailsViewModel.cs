using PumaCoinCatalog.Web.Models.CbCoinData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PumaCoinCatalog.Web.Models.CbCollection
{
    public class CbChecklistTypeDetailsViewModel
    {
        public int DenominationId { get; set; }
        public string DenominationTitle { get; set; }
        public int VarietyId { get; set; }
        public string VarietyTitle { get; set; }
        public int TypeId { get; set; }
        public string TypeTitle { get; set; }
        public int ChecklistId { get; set; }
        public string ChecklistTitle { get; set; }

        public short BeginDate { get; set; }
        public short EndDate { get; set; }
        public string MetalComposition { get; set; }
        public float Diameter { get; set; }
        public float Mass { get; set; }
        public decimal MeltValue { get; set; }        
        public string SourceUri { get; set; }
        
        public CbCoinImageViewModel ImageViewModel { get; set; }
    }
}