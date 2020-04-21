using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelSubMaterialOption
    {
        public int ModelID { get; set; }
        public int SubMaterialID { get; set; }
        public string SubMaterialUD { get; set; }
        public string SubMaterialNM { get; set; }
        public bool IsSelected { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
    }
}