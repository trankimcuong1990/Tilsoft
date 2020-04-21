using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceMng.DTO
{
    public class FactoryOrderDetailSearchResult
    {
        public int FactoryOrderDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? TotalQuantity { get; set; }
        public string BLNo { get; set; }
        public string BookingUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ItemName { get; set; }
        public int? BookingID { get; set; }
        public int? SupplierID { get; set; }
        public string ClientUD { get; set; }
        public decimal? PurchasingPrice { get; set; }
    }
}
