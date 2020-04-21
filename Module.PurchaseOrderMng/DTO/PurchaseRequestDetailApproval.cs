using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseRequestDetailApproval
    {
        public int PurchaseRequestDetailID { get; set; }

        public int? PurchaseRequestID { get; set; }

        public int? ProductionItemID { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        public decimal? StockQnt { get; set; }

        public decimal? RequestQnt { get; set; }

        public string UnitNM { get; set; }

        public string ETA { get; set; }

        public decimal? OrderQnt { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? TotalAmount { get; set; }

        public int PrimaryID { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }

        public List<PurchaseRequestWorkOrderDetail> PurchaseRequestWorkOrderDetails { get; set; }
    }
    public class PurchaseRequestData
    {
        public List<PurchaseRequestDetailApproval> PurchaseRequestDetailApprovals { get; set; }
        public int typePQAndPR { get; set; }
    }
}
