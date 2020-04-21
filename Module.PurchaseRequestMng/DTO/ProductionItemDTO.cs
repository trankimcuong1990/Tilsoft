using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class ProductionItemDTO
    {
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public int? SuggestedFactoryRawMaterialID { get; set; }
        public decimal? RequestQnt { get; set; }
        public List<WorkOrderDTO> WorkOrderDTOs { get; set; }
        public decimal? OrderQnt { get; set; }
        public decimal? StockQnt { get; set; }
        public bool? IsApproved { get; set; }
        public string UnitNM { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ETA { get; set; }
        public int? ProductionItemGroupID { get; set; }
    }
}
