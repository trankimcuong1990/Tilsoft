using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class ModelDTO
    {
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string Season { get; set; }
        public string RangeName { get; set; }
        public string Collection { get; set; }
        public int? CataloguePageNo { get; set; }
        public int? ModelStatusID { get; set; }
        public string ModelStatusNM { get; set; }
    }
}
