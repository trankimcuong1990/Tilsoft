using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class ModelSparepartDTO
    {
        public int ModelSparepartID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string PartUD { get; set; }
        public string Description { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
    }
}
