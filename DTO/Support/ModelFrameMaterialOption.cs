using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelFrameMaterialOption
    {
        public int FrameMaterialID { get; set; }
        public string FrameMaterialUD { get; set; }
        public string FrameMaterialNM { get; set; }
        public bool IsSelected { get; set; }
        public bool IsStandard { get; set; }
        public int MaterialOptionID { get; set; }
    }
}