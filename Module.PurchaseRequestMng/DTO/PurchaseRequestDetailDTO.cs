using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class PurchaseRequestDetailDTO
    {
        public int? PurchaseRequestDetailID { get; set; }
        public int? PurchaseRequestID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? SuggestedFactoryRawMaterialID { get; set; }
        public decimal? RequestQnt { get; set; }
        public string ETA { get; set; }
        public string Remark { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public decimal? StockQnt { get; set; }
        public decimal? OrderQnt { get; set; }
        public bool? IsApproved { get; set; }
        public string UnitNM { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int? ProductionItemGroupID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? BranchID { get; set; }
        public long PrimaryID { get; set; }

        public List<DTO.PurchaseRequestWorkOrderDetailDTO> PurchaseRequestWorkOrderDetailDTOs { get; set; }

        public PurchaseRequestDetailDTO()
        {
            PurchaseRequestWorkOrderDetailDTOs = new List<PurchaseRequestWorkOrderDetailDTO>();
        }
    }
}
