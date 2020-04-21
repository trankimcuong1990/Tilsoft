using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DTO.Support
{
    public class ModelCushionOption
    {
        public int CushionID { get; set; }
        public string CushionUD { get; set; }
        public string CushionNM { get; set; }
        public bool IsSelected { get; set; }
        public bool IsStandard { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
    }
}