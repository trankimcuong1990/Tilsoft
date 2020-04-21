using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class FrameMaterialColorOptionDTO
    {
        public int FrameMaterialColorID { get; set; }
        public string FrameMaterialColorUD { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public string FrameMaterialUD { get; set; }
        public string FrameMaterialNM { get; set; }
        public int FrameMaterialOptionID { get; set; }
        public string FrameMaterialOptionUD { get; set; }
        public string FrameMaterialOptionNM { get; set; }
        public int IsStandard { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public Nullable<int> MaterialOptionID { get; set; }
    }
}
