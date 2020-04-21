using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseQuotationAndSupplierDto
    {
        public long KeyID { get; set; }
        public Nullable<int> PurchaseQuotationDetailID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string TypeName { get; set; }
        public Nullable<int> MaterialsPriceID { get; set; }
        public Nullable<int> MaterialHistoryID { get; set; }
    }
    public class PurchaseQuotationData
    {
        public List<PurchaseQuotationAndSupplierDto> purchaseQuotationAndSupplierDtos { get; set; }
        public int typePQAndPR { get; set; }
    }
}
