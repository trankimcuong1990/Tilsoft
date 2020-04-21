using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class OutSourcingCostTypeDTO
    {
        public int OutsourcingCostTypeID { get; set; }
        public string OutsourcingCostTypeUD { get; set; }
        public string OutsourcingCostTypeNM { get; set; }
        public string OutsourcingCostTypeNMEN { get; set; }
        public Nullable<int> ProductionItemGroupID { get; set; }
    }
}
