using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FrameMaterialColorMng.DTO
{
    public class FrameMaterialColorSearchResult
    {
        public int FrameMaterialColorID { get; set; }
        public string FrameMaterialColorUD { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdateName { get; set; }
        public decimal Total { get; set; }
    }
}
