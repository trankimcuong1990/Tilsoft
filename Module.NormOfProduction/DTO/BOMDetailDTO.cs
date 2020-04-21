using System;

namespace Module.NormOfProduction.DTO
{
    public class BOMDetailDTO
    {
        public int BOMID { get; set; }
        public Nullable<int> ParentBOMID { get; set; }
        public Nullable<decimal> Norm { get; set; }
        public Nullable<decimal> TotalNorm { get; set; }
        public Nullable<decimal> Delta { get; set; }
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public Nullable<decimal> WastagePercent { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public int UnitID { get; set; }
        public string UnitNM { get; set; }
    }
}
