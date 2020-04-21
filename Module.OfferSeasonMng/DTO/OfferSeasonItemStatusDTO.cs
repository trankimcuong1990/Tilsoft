using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class OfferSeasonItemStatusDTO
    {
        public Nullable<int> ItemStatus { get; set; }
        public string ItemStatusText { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
