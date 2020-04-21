using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class FrameMaterialOptionDTO
    {
        public int FrameMaterialID { get; set; }
        public string FrameMaterialUD { get; set; }
        public string FrameMaterialNM { get; set; }
        public Nullable<int> MaterialOptionID { get; set; }
        public int IsStandard { get; set; }
    }
}
