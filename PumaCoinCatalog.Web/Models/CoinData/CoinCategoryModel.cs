using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CoinData
{
    public class CoinCategoryModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Base64Image { get; set; }
        public int SortOrder { get; set; }

        public IList<CoinTypeModel> Types { get; set; }

        // for breadcrumb
        public string CollectionTitle { get; set; }
        public Guid CollectionId { get; set; }
    }
}