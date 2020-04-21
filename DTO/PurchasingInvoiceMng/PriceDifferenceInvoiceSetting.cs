using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PurchasingInvoiceMng
{
    public class PriceDifferenceInvoiceSetting
    {
        public int PriceDifferenceInvoiceSettingID { get; set; }
        public string Season { get; set; }
        public int? FactoryID { get; set; }
        public decimal? Rate { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }
        public string FactoryUD { get; set; }
    }
}
