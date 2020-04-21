using System;
using System.Collections.Generic;

namespace Module.NormOfProduction.DTO
{
    public class BOMDTO
    {
        public int BOMID { get; set; }
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int WorkOrderID { get; set; }
        public Nullable<int> Planning { get; set; }
        public Nullable<decimal> Finished { get; set; }      
        public List<BOMDetailDTO> BOMDetailDTOs { get; set; }
        public bool HasBOMDetail { get; set; }
        public int? WorkCenterID { get; set; }
    }
}
