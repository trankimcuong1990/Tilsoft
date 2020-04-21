using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class RelatedFactoryOrderDetailDTO
    {
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public Nullable<int> TotalOfferSeasonItem { get; set; }
        public Nullable<int> TotalFactoryOrderQnt { get; set; }
    }
}
