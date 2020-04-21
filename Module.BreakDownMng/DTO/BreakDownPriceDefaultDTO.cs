using System;

namespace Module.BreakDownMng.DTO
{
    public class BreakDownPriceDefaultDTO
    {
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemVNNM { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public int BreakdownCategoryID { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }

        public Nullable<decimal> QuantityAVF { get; set; }
        public Nullable<decimal> UnitPriceAVF { get; set; }
        public Nullable<decimal> Quantity1 { get; set; }
        public Nullable<decimal> UnitPrice1 { get; set; }
        public Nullable<decimal> Quantity2 { get; set; }
        public Nullable<decimal> UnitPrice2 { get; set; }
        public Nullable<decimal> Quantity3 { get; set; }
        public Nullable<decimal> UnitPrice3 { get; set; }
    }
}
