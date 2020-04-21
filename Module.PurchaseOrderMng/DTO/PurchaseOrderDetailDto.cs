using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseOrderDetailDto
    {
        public int? PurchaseOrderDetailID { get; set; }
        public int? PurchaseOrderID { get; set; }
        public int? PurchaseRequestDetailID { get; set; }
        public decimal? OrderQnt { get; set; }
        public string ETA { get; set; }
        public string Remark { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? ProductionItemID { get; set; }
        public int? PurchaseQuotationDetailID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public decimal? StockQnt { get; set; }        
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public int? UnitID { get; set; }
        public decimal? RequestQnt { get; set; }
        public bool? IsChangePrice { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? TotalReceived { get; set; }
        public string ID { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string TypeName { get; set; }
        public int? MaterialsPriceID { get; set; }
        public int? MaterialHistoryID { get; set; }
        public List<DTO.PurchaseOrderWorkOrderDetailDto> PurchaseOrderWorkOrderDetailDTOs { get; set; }
        public List<DTO.PurchaseOrderDetailReceivingPlanDto> PurchaseOrderDetailReceivingPlanDtos { get; set; }
        public List<DTO.PurchaseOrderDetailPriceHistoryDto> PurchaseOrderDetailPriceHistoryDtos { get; set; }
    }
}
