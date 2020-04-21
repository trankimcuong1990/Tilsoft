using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostTypeMng.DTO
{
    public class OutsourcingCostTypeDTO
    {
        public int OutsourcingCostTypeID { get; set; }
        public string OutsourcingCostTypeUD { get; set; }
        public string OutsourcingCostTypeNM { get; set; }
        public string OutsourcingCostTypeNMEN { get; set; }
        public int? ProductionItemGroupID { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdaterName { get; set; }
    }
}
