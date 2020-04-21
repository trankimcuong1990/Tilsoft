using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class DefaultPricePurchaseQuotationDetailData
    {
        public int PurchaseQuotationDetailID { get; set; }

        public int? ProductionItemID { get; set; }

        public int? FactoryRawMaterialID { get; set; }

        public string FactoryRawMaterialUD { get; set; }

        public decimal? OrderQnt { get; set; }

        public decimal? UnitPrice { get; set; }

        public bool? IsDefault { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }
    }
}
