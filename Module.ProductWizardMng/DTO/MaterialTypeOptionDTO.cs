using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class MaterialTypeOptionDTO
    {
        public int MaterialTypeID { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeNM { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public int IsStandard { get; set; }
    }
}
