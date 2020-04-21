using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class PurchaseOrderDTO
    {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public int? Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string Address { get; set; }
        public int? CompanyID { get; set; }
        public decimal? TotalQntWithPurchaseOrder { get; set; }
        public List<PurchaseOrderDetailDTO> PurchaseOrderDetailDTOs { get; set; }
        public PurchaseOrderDTO()
        {
            PurchaseOrderDetailDTOs = new List<PurchaseOrderDetailDTO>();
        }
    }

    public class PurchaseOrderDetailDTO
    {
        public int PurchaseOrderDetailID { get; set; }
        public int? PurchaseOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public decimal? OrderQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalReceive { get; set; }
        public List<PurchaseOrderWorkOrderDetailDTO> PurchaseOrderWorkOrderDetailDTOs { get; set; }
        public PurchaseOrderDetailDTO()
        {
            PurchaseOrderWorkOrderDetailDTOs = new List<PurchaseOrderWorkOrderDetailDTO>();
            productionItemUnits = new List<ProductionItemUnit>();
        }

        public decimal? StockQnt { get; set; }
        public int? BranchID { get; set; }
        public decimal? ConversionFactorMainUnit { get; set; }

        public List<DTO.ProductionItemUnit> productionItemUnits { get; set; }
    }

    public class PurchaseOrderWorkOrderDetailDTO
    {
        public int PurchaseOrderWorkOrderDetailID { get; set; }
        public int? PurchaseOrderDetailID { get; set; }
        public decimal? PurchaseOrderWorkOrderQnt { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string FinishDate { get; set; }
        public decimal? TotalNorm { get; set; }
    }
}
