using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class PurchaseOrderApprove
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public string FactoryRawMaterialOfficialNM { get; set; }

        public string FactoryRawMaterialShortNM { get; set; }

        public string Address { get; set; }

        public decimal? TotalQntWithPurchaseOrder { get; set; }

        public List<PurchaseOrderDetailApprove> PurchaseOrderDetailApproves { get; set; }
    }

    public class PurchaseOrderDetailApprove
    {
        public int? ProductionItemID { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        public string UnitNM { get; set; }

        public decimal? OrderQnt { get; set; }

        public int PurchaseOrderDetailID { get; set; }

        public int PurchaseOrderID { get; set; }

        public int? FactoryWarehouseID { get; set; }

        public decimal? UnitPrice { get; set; }

        public List<PurchaseOrderDetailApproveProductionItemUnitData> ProductionItemUnits { get; set; }
    }

    public class PurchaseOrderDetailApproveProductionItemUnitData
    {
        public int PrimaryID { get; set; }

        public int? ProductionItemID { get; set; }

        public int? UnitID { get; set; }

        public string UnitNM { get; set; }

        public decimal? ConversionFactor { get; set; }
    }
}
