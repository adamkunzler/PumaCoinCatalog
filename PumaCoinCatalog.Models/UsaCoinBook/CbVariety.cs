﻿using System.Collections.Generic;

namespace PumaCoinCatalog.Models.UsaCoinBook
{
    public class CbVariety
    {
        public CbVariety()
        {
            Types = new List<CbType>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string SourceUri { get; set; }
        public string ObverseImageUri { get; set; }
        public string ReverseImageUri { get; set; }

        public virtual CbDenomination Denomination { get; set; }
        public virtual IList<CbType> Types { get; set; }
    }
}
