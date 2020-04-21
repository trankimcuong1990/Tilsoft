using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class FrameMaterialOption
    {
        public int FrameMaterialOptionID { get; set; }
        public string FrameMaterialOptionUD { get; set; }
        public string FrameMaterialOptionNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
    }
}