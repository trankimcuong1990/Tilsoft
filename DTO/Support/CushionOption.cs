using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class CushionOption
    {
        public int CushionID { get; set; }
        public string CushionUD { get; set; }
        public string CushionNM { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public bool IsSelected { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
    }
}