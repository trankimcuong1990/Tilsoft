using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class SaleOrderDetailSparepartDTO
    {
        public int SaleOrderDetailSparepartID { get; set; }
        public int? SaleOrderID { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int? Client { get; set; }
        public string Season
        {
            get; set;
        }
    }
}
