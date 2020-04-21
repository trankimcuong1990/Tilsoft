using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class CushionColorOptionDTO
    {
        public int CushionColorID { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }
        public int IsStandard { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public Nullable<int> CushionTypeID { get; set; }
    }
}
