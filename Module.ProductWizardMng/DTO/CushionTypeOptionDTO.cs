using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class CushionTypeOptionDTO
    {
        public Nullable<int> CushionTypeID { get; set; }
        public string CushionTypeNM { get; set; }
        public int IsStandard { get; set; }
    }
}
