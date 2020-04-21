using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SubMaterialColorMng
{
    public class SubMaterialColor
    {
        public int SubMaterialColorID { get; set; }
        public string SubMaterialColorUD { get; set; }
        public string SubMaterialColorNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdateName { get; set; }
        public int Total { get; set; }
    }
}