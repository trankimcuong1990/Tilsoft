using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng.DTO
{
    public class ItemSearchResult
    {
        public int? OfferLineID { get; set; }
        public int? OfferLineSparepartID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}
