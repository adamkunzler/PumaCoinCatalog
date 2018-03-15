﻿using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CbCoinData
{
    public class CbVarietyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ObverseImageUri { get; set; }
        public string ReverseImageUri { get; set; }

        public IList<CbTypeViewModel> Types { get; set; }

        // for breadcrumb
        public string CountryTitle { get; set; }
        public int DenominationId { get; set; }
        public string DenominationTitle { get; set; }
    }
}