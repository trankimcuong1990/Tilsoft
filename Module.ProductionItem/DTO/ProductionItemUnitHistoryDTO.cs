using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class ProductionItemUnitHistoryDTO
    {
        public int ProductionItemUnitHistoryID { get; set; }
        public Nullable<int> ProductionItemUnitID { get; set; }
        public string ValidFromHistory { get; set; }
        public Nullable<decimal> HsqdHistory { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdateDate { get; set; }
        public string Creator { get; set; }
        public string Remark { get; set; }
    }
}
