using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownPriceHistoryDetailDTO
    {
        public int BreakdownPriceHistoryDetailID { get; set; }
        public Nullable<int> BreakdownPriceHistoryID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> BreakdownCategoryID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
