using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryOrderProductionStatusRpt.DTO
{
    public class FactoryOrderProductionStatus
    {
        public object KeyID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public int? OrderQnt { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ProductionQnt { get; set; }
        public decimal? ProducedQnt { get; set; }
        public decimal? RemainQnt { get; set; }
    }
}
