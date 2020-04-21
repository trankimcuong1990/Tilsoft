using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingStandardCostFeeMng.DTO
{
    public class OutsourcingStandardCostFeeDTO
    {
        public int OutsourcingStandardCostFeeID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> OutsourcingCostID { get; set; }
        public Nullable<int> OursourcingCostTypeID { get; set; }
        public Nullable<decimal> StandardPrice { get; set; }
        public string ValidFrom { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string OutsourcingCostUD { get; set; }
        public string OutsourcingCostNM { get; set; }
        public string OutsourcingCostNMEN { get; set; }
        public string OutsourcingCostTypeUD { get; set; }
        public string OutsourcingCostTypeNM { get; set; }
        public string OutsourcingCostTypeNMEN { get; set; }
        public string UpdaterNM { get; set; }
        public string ConfirmerNM { get; set; }
    }
}
