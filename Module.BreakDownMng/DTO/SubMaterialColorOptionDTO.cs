using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class SubMaterialColorOptionDTO
    {
        public int SubMaterialColorID { get; set; }
        public string SubMaterialColorNM { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public int DisplayIndex { get; set; }
        public int SubMaterialID { get; set; }
        public string ThumbnailLocation { get; set; }
    }
}
