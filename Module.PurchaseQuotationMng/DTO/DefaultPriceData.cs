using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class DefaultPriceData
    {
        public int? CountPurchasing { get; set; }
        public List<DefaultPricePurchaseQuotationDetailData> DefaultPricePurchaseQuotationDetail { get; set; }
        public List<DefaultPriceProductionItemData> DefaultPriceProductionItem { get; set; }

        public DefaultPriceData()
        {
            DefaultPricePurchaseQuotationDetail = new List<DefaultPricePurchaseQuotationDetailData>();
            DefaultPriceProductionItem = new List<DefaultPriceProductionItemData>();
        }
    }
}
