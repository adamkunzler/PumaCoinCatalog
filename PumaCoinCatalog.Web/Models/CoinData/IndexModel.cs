using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PumaCoinCatalog.Web.Models.CoinData
{
    public class CoinCollectionModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public IList<CoinCategoryModel> Categories { get; set; }
    }

    public class CoinCategoryModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }        
        public string Base64Image { get; set; }
        public int SortOrder { get; set; }

        public IList<CoinTypeModel> Types { get; set; }
    }

    public class CoinTypeModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }        
        public string Base64Image { get; set; }
        public int SortOrder { get; set; }

        public IList<CoinModel> Coins { get; set; }
    }

    public class CoinModel
    {
        public Guid Id { get; set; }
        public string Year { get; set; }
        public string Variety { get; set; }
        public int Mintage { get; set; }
        public int SortOrder { get; set; }
    }

    public class IndexModel
    {
        public CoinCollectionModel Collection { get; set; }
    }
}