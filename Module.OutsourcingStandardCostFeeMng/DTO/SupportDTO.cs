using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingStandardCostFeeMng.DTO
{
    public class SupportDTO
    {
        public SupportDTO()
        {
            outSourcingCostSPs = new List<OutSourcingCostSP>();
            outSourcingCostTypeSPs = new List<OutSourcingCostTypeSP>();
        }
        public List<DTO.OutSourcingCostSP> outSourcingCostSPs { get; set; }
        public List<DTO.OutSourcingCostTypeSP> outSourcingCostTypeSPs { get; set; }
    }

    public class OutSourcingCostSP
    {
        public int OutsourcingCostID { get; set; }
        public string OutsourcingCostUD { get; set; }
        public string OutsourcingCostNM { get; set; }
        public string OutsourcingCostNMEN { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
    }
    public class OutSourcingCostTypeSP
    {
        public int OutsourcingCostTypeID { get; set; }
        public string OutsourcingCostTypeUD { get; set; }
        public string OutsourcingCostTypeNM { get; set; }
        public string OutsourcingCostTypeNMEN { get; set; }
        public Nullable<int> ProductionItemGroupID { get; set; }
    }
}
