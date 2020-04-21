using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class SelectedCategoryOptionDTO
    {
        public int materialCostOptionID { get; set; }
        public int frameCostOptionID { get; set; }
        public int subMaterialCostOptionID { get; set; }
        public int cushionCostOptionID { get; set; }
        public int packingCostOptionID { get; set; }
        public int hardwareCostOptionID { get; set; }
        public int otherMaterialCostOptionID { get; set; }
        public int fscCostOptionID { get; set; }
        public int specialRequirementCostOptionID { get; set; }
        public int managementCostOptionID { get; set; }
    }
}
