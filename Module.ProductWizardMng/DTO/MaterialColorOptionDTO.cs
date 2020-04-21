using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class MaterialColorOptionDTO
    {
        public int MaterialColorID { get; set; }
        public string MaterialColorUD { get; set; }
        public string MaterialColorNM { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeNM { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public string MaterialUD { get; set; }
        public string MaterialNM { get; set; }
        public int MaterialOptionID { get; set; }
        public string MaterialOptionUD { get; set; }
        public string MaterialOptionNM { get; set; }
        public int IsStandard { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
    }
}
