using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class WorkOrderDto
    {
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public decimal? RequestingQnt { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
    }
}
