using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class SparepartDTO
    {
        public int SparepartID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string Season { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> PartID { get; set; }
    }
}
