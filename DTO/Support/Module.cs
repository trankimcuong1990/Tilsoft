using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class Module
    {
        public int ModuleID { get; set; }
        public string DisplayText { get; set; }
        public int ParentID { get; set; }
        public int DisplayOrder { get; set; }
    }
}