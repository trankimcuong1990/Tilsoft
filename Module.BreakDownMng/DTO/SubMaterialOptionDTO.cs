using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class SubMaterialOptionDTO
    {
        public int SubMaterialID { get; set; }
        public string SubMaterialNM { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public int DisplayIndex { get; set; }
    }
}
