using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Web.Models.CoinData
{
    public class CoinCollectionModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public IList<CoinCategoryModel> Categories { get; set; }
    }
}