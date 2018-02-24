using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CoinData
{
    public class CoinTypeModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Base64Image { get; set; }
        public int SortOrder { get; set; }
        public decimal BullionValue { get; set; }

        public IList<CoinModel> Coins { get; set; }

        // for breadcrumb
        public string CollectionTitle { get; set; }
        public Guid CollectionId { get; set; }
        public string CategoryTitle { get; set; }
        public Guid CategoryId { get; set; }
    }
}