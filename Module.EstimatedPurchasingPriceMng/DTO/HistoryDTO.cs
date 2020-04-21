using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Module.EstimatedPurchasingPriceMng.DTO
{
    public class HistoryDTO
    {
        public int EstimatedPurchasingPriceHistoryID { get; set; }
        public decimal EstimatedPrice { get; set; }

        [MaxLength(255, ErrorMessage = "Remark can not exceed 255 chars!")]
        public string Remark { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
