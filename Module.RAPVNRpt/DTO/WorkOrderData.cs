using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RAPVNRpt.DTO
{
    public class WorkOrderData
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public int? TotalQuantity { get; set; }
    }
}
