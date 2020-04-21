using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelFrameMaterialColorOption
    {
        public int FrameMaterialColorID { get; set; }
        public string FrameMaterialColorUD { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public int FrameMaterialID { get; set; }
        public int MaterialOptionID { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public bool IsSelected { get; set; }

        public string FrameMaterialUD { get; set; }
        public string FrameMaterialNM { get; set; }
        public string FrameMaterialColorNM_Origin { get; set; }
        public bool IsStandard { get; set; }
        public int FrameMaterialOptionID { get; set; }
    }
}