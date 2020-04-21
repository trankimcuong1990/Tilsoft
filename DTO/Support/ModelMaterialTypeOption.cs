using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelMaterialTypeOption
    {
        public int MaterialTypeID { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeNM { get; set; }
        public int MaterialID { get; set; }
        public bool IsSelected { get; set; }
        public bool IsStandard { get; set; }
    }
}