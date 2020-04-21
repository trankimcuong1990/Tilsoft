using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class SupplierDTO
    {
        public int PrimaryKey { get; set; }
        public int ProductionItemID { get; set; }
        public int OrderQnt { get; set; }
        public int UnitPrice { get; set; }
        public bool IsDefault { get; set; }
        public int FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public int PurchaseQuotationDetailID { get; set; }
    }
}
