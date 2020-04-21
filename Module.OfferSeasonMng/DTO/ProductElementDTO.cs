using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class ProductElementDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> ProductElementID { get; set; }
        public string ProductElementNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
