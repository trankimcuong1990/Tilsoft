using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class SubMaterialOptionDTO
    {
        public Nullable<int> SubMaterialID { get; set; }
        public string SubMaterialUD { get; set; }
        public string SubMaterialNM { get; set; }
        public int IsStandard { get; set; }
    }
}
