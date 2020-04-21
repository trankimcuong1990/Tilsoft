using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseOrderWorkOrderDetailDto
    {
        public int? PurchaseOrdertWorkOrderDetailID { get; set; }
        public int? PurchaseOrderDetailID { get; set; }
        public int? WorkOrderID { get; set; }
        public decimal? OrderQnt { get; set; }
        public string Remark { get; set; }

        public string WorkOrderUD { get; set; }
        public decimal? RequestedQnt { get; set; }
        public string FinishDate { get; set; }
        public decimal? TotalNorm { get; set; }
        public long PrimaryKey { get; set; }
    }
}
