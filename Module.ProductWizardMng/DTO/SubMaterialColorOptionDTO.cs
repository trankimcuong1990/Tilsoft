using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class SubMaterialColorOptionDTO
    {
        public Nullable<int> SubMaterialColorID { get; set; }
        public string SubMaterialColorUD { get; set; }
        public string SubMaterialColorNM { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public string SubMaterialUD { get; set; }
        public string SubMaterialNM { get; set; }
        public Nullable<int> SubMaterialOptionID { get; set; }
        public string SubMaterialOptionUD { get; set; }
        public string SubMaterialOptionNM { get; set; }
        public int IsStandard { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
    }
}
