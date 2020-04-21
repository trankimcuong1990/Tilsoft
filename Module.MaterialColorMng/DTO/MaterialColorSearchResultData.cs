using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialColorMng.DTO
{
    public class MaterialColorSearchResultData
    {
        public int MaterialColorID { get; set; }
        public string MaterialColorUD { get; set; }
        public string MaterialColorNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdateName { get; set; }
        public decimal Total { get; set; }
    }
}
