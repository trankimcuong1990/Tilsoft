using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class MaterialColorOptionDTO
    {
        public int MaterialColorID { get; set; }
        public string MaterialColorUD { get; set; }
        public string MaterialColorNM { get; set; }
        public int MaterialID { get; set; }
        public int MaterialTypeID { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public string ThumbnailLocation { get; set; }
        public int DisplayIndex { get; set; }
    }
}
