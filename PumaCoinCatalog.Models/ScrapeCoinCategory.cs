﻿using System;
using System.Collections.Generic;

namespace PumaCoinCatalog.Models
{
    public class ScrapeCoinCategory
    {
        public ScrapeCoinCategory()
        {
            Id = Guid.NewGuid();
            CoinTypes = new List<ScrapeCoinType>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }                
        public int SortOrder { get; set; }
        public string Base64Image { get; set; }

        public virtual IList<ScrapeCoinType> CoinTypes { get; set; }
        public virtual ScrapeCoinCollection CoinCollection { get; set; }

        public override string ToString()
        {
            return $"Category - {Title}";
        }
    }
}
