using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class BOMDTO
    {
        public int BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public int? WorkOrderStatusID { get; set; }
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? DefaultFactoryWarehouseID { get; set; }
        public decimal? BOMQnt { get; set; }
        public decimal? BOMQntTotal { get; set; }
        public decimal? BOMQuantity { get; set; }
        public decimal? BOMQuantityTotal { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public int? WorkCenterID { get; set; }
        public bool? IsExceptionAtConfirmFrameStatus { get; set; }
        public int? ProductionTeamID { get; set; }

        public int? ParentBOMID { get; set; }
        public int? ParentProductionItemID { get; set; }
        public string ParentProductionItemUD { get; set; }
        public string ParentProductionItemNM { get; set; }
        public int? ParentWorkCenterID { get; set; }        
        public bool? ParentIsExceptionAtConfirmFrameStatus { get; set; }

        public decimal? TotalDelivery { get; set; }
        public decimal? StockQnt { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public List<ProductionItemUnitBOM> ProductionItemUnits { get; set; }
        public decimal? UnitPrice { get; set; }
        public int PrimaryID { get; set; }
        public decimal? TotalDeliverWorkOrder { get; set; }

        public BOMDTO()
        {
            ProductionItemUnits = new List<ProductionItemUnitBOM>();
        }
    }
}
