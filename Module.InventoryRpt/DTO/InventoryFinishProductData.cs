using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.InventoryRpt.DTO
{
    public class InventoryFinishProductData
    {
        public int PrimaryID { get; set; }

        public int? WorkOrderID { get; set; }

        public string WorkOrderUD { get; set; }

        public int? ProductID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public int? ClientID { get; set; }

        public string ClientUD { get; set; }

        public int? SaleOrderID { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public int? OrderQnt { get; set; }

        public decimal? BeginningQnt { get; set; }

        public decimal? ReceivingQnt { get; set; }

        public decimal? DeliveringQnt { get; set; }

        public decimal? EndingInventoryQnt { get; set; }
    }
}
