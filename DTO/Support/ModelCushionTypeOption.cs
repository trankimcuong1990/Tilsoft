using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelCushionTypeOption
    {
        public int CushionTypeID { get; set; }
        public string CushionTypeNM { get; set; }
        public bool IsStandard { get; set; }
        public bool IsSelected { get; set; }

    }
}