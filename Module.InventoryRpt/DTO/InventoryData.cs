using System.Collections.Generic;

namespace Module.InventoryRpt.DTO
{
    public class InventoryData
    {
        public int? ProductionItemID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public int? UnitID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? ProductID { get; set; }

        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public string UnitNM { get; set; }
        public string ClientUD { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public decimal? FrameWeight { get; set; }
        public decimal? StockedQnt { get; set; }
        public decimal? ReceivedQnt { get; set; }
        public decimal? DeliveredQnt { get; set; }
        public decimal? EndedQnt { get; set; }
        public int? Qnt40HC { get; set; }        
        public decimal? StockedCont { get; set; }
        public decimal? ReceivedCont { get; set; }
        public decimal? DeliveredCont { get; set; }
        public decimal? EndedCont { get; set; }

        public int? ProductionTeamID { get; set; }
        public string ProductionTeamNM { get; set; }

        public List<DTO.InventoryDetailData> InventoryDetails { get; set; }
        public bool IsHideDetail { get; set; }

        public InventoryData()
        {
            InventoryDetails = new List<InventoryDetailData>();
        }
    }
}
