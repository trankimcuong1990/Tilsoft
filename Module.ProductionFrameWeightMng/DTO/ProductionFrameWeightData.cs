using System.Collections.Generic;

namespace Module.ProductionFrameWeightMng.DTO
{
    public class ProductionFrameWeightData
    {
        public int ProductionFrameWeightID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? FrameWeight { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public string EmployeeNM { get; set; }

        public List<ProductionFrameWeightHistoryData> ProductionFrameWeightHistory { get; set; }

        public ProductionFrameWeightData()
        {
            ProductionFrameWeightHistory = new List<ProductionFrameWeightHistoryData>();
        }
    }
}
