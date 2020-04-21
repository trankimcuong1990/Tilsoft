using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelBackCushionOption
    {
        public int BackCushionID { get; set; }
        public string BackCushionUD { get; set; }
        public string BackCushionNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public int ProductGroupID { get; set; }
        public int DisplayIndex { get; set; }
        public bool IsSelected { get; set; }
    }
}