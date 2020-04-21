using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingStandardCostFeeMng.DTO
{
    public class OutSourcingModelDetailSearch
    {
        public int OutsourcingStandardCostFeeID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> OutsourcingCostID { get; set; }
        public Nullable<int> OursourcingCostTypeID { get; set; }
        public Nullable<decimal> StandardPrice { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string ModelUD { get; set; }
        public long KeyID { get; set; }
    }
}
