using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionSummaryRpt.DTO
{
    public class ProductionSummaryDTO
    {
        public int? ProductID { get; set; }
        public string FactoryUD { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string WorkOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? OrderQnt { get; set; }
        public int? WorkOrderQnt { get; set; }
        public int PrimaryID { get; set; }
        public decimal? Order40HC { get; set; }
        public decimal? Pro40HC { get; set; }
        public int? WorkOrderStatusID { get; set; }
        public int? PreOrderWorkOrderID { get; set; }
        public int? WorkOrderID { get; set; }
        public string PreOrderWorkOrderUD { get; set; }
    }
}
