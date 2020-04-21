using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelSubMaterialColorOption
    {
        public int ModelID { get; set; }
        public int SubMaterialColorID { get; set; }
        public string SubMaterialColorUD { get; set; }
        public string SubMaterialColorNM { get; set; }
        public int SubMaterialID { get; set; }
        public int SubMaterialOptionID { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public bool IsSelected { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }

        public string SubMaterialUD { get; set; }
        public string SubMaterialNM { get; set; }
        public string SubMaterialColorNM_Origin { get; set; }
    }
}