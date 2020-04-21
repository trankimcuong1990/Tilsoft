using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByItemRpt.DTO
{
    public class PiConfirmedByItemCategory
    {
        public int ProductCategoryID { get; set; }
        public string ProductCategoryNM { get; set; }
        public decimal Delta5Amount { get; set; }
        public decimal PurchasingAmount { get; set; }
        public decimal SaleAmountUSD { get; set; }
        public Nullable<decimal> Delta5Percent { get; set; }
        public decimal TotalCont { get; set; }
        public Nullable<int> TotalItem { get; set; }
    }
}
