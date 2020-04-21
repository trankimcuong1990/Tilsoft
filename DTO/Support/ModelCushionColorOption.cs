using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelCushionColorOption
    {
        public int CushionColorID { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }

        public int CushionTypeID { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public bool IsSelected { get; set; }
        public bool IsStandard { get; set; }
    }
}