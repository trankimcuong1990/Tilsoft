using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class OfferSeasonDetailPriceHistoryDTO
    {
        public int OfferSeasonDetailPriceHistoryID { get; set; }
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
    }
}
