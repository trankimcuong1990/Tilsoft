using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelMaterialColorOption
    {
        public int MaterialColorID { get; set; }
        public string MaterialColorUD { get; set; }
        public string MaterialColorNM { get; set; }
        public int MaterialTypeID { get; set; }
        public int MaterialID { get; set; }
        public int MaterialOptionID { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public bool IsSelected { get; set; }

        public string MaterialUD { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM_Origin { get; set; }
        public bool IsStandard { get; set; }
    }
}