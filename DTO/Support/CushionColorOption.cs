using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class CushionColorOption
    {
        public int CushionColorID { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
    }
}