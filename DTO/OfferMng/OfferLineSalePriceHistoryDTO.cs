using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class OfferLineSalePriceHistoryDTO
    {
        public int OfferLineSalePriceHistoryID { get; set; }
        public decimal SalePrice { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
