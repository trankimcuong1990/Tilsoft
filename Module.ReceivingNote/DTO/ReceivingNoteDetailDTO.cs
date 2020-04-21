using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class ReceivingNoteDetailDTO
    {
        public int ReceivingNoteDetailID { get; set; }

        public int? ReceivingNoteID { get; set; }
        public int? PurchaseOrderDetailID { get; set; }        

        public int? ProductionItemID { get; set; }
        public decimal? OldQuantity { get; set; }

        public decimal? Quantity { get; set; }

        public int? FactoryWarehousePalletID { get; set; }

        public string Description { get; set; }

        public int? ToFactoryWarehouseID { get; set; }

        public int? BOMID { get; set; }

        public int? QNTBarCode { get; set; }

        public decimal? QtyByUnit { get; set; }

        public int? UnitID { get; set; }

        public int? WorkOrderID { get; set; }

        public decimal? UnitPrice { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        public string Unit { get; set; }

        public string UnitNM { get; set; }

        public int? ProductionItemTypeID { get; set; }

        public string ProductionItemTypeNM { get; set; }

        public string FactoryWarehousePalletNM { get; set; }

        public string WorkOrderUD { get; set; }

        public decimal? BOMQnt { get; set; }

        public decimal? BOMQntTotal { get; set; }

        public decimal? StockQnt { get; set; }

        public decimal? TotalReceive { get; set; }

        public string ParentProductionItemUD { get; set; }

        public string ParentProductionItemNM { get; set; }

        public int? ParentProductionItemID { get; set; }

        public string ToFactoryWarehouseNM { get; set; }

        public decimal? FrameWeight { get; set; }

        public bool? IsHasFrameWeight { get; set; }

        public List<ProductionItemUnit> ProductionItemUnits { get; set; }

        public List<ReceivingNoteWorkOrderDetailDTO> ReceivingNoteWorkOrderDetailDTOs { get; set; }

        public List<ReceivingNoteDetailSubUnitDTO> ReceivingNoteDetailSubUnitDTOs { get; set; }

        public decimal? OrderQnt { get; set; }

        public decimal? OtherPrice { get; set; }
        public int? ReasonOtherPriceID { get; set; }

        public ReceivingNoteDetailDTO()
        {
            ProductionItemUnits = new List<ProductionItemUnit>();
            ReceivingNoteWorkOrderDetailDTOs = new List<ReceivingNoteWorkOrderDetailDTO>();
            ReceivingNoteDetailSubUnitDTOs = new List<ReceivingNoteDetailSubUnitDTO>();
        }

        public decimal? TotalReceiveWorkOrder { get; set; }
        public int? SubUnitID { get; set; }
        public decimal? SubQnt { get; set; }
        public string SubUnitNM { get; set; }
        public decimal? ConversionFactorAffter { get; set; }
        public decimal? ConversionFactorMainUnit { get; set; }
        public decimal? ConversionFactorSubUnit { get; set; }
    }
}
