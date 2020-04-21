using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class MaterialOptionDTO
    {
        public int MaterialID { get; set; }
        public string MaterialUD { get; set; }
        public string MaterialNM { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public int DisplayIndex { get; set; }
    }
}
