using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostMng.DTO
{
    public class OutsourcingCostSearchDTO
    {
        public int OutsourcingCostID { get; set; }
        public string OutsourcingCostUD { get; set; }
        public string OutsourcingCostNM { get; set; }
        public string OutsourcingCostNMEN { get; set; }
        public int? WorkCenterID { get; set; }
        public string WorkCenterNM { get; set; }
        public bool? IsAdditionalCost { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdaterName { get; set; }
    }
}
