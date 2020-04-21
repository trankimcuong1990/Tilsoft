using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByItemRpt.DTO
{
    public class CommercialInvoiceByCategories
    {
        public string Season { get; set; }
        public string ProductCategoryNM { get; set; }
        public Nullable<decimal> CIAmountInEUR { get; set; }
        public Nullable<decimal> CITotalCont { get; set; }
        public Nullable<int> CITotalItem { get; set; }
        public Nullable<decimal> LCCostAmountInEUR { get; set; }
        public Nullable<decimal> InterestAmountInEUR { get; set; }
        public Nullable<decimal> PurchasingAmountInEUR { get; set; }
        public Nullable<decimal> DeltaAfterAllInEUR { get; set; }
        public Nullable<decimal> DeltaAfterAllPercent { get; set; }
    }
}
