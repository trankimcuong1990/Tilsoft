using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ModelPackagingMethodOption
    {
        public int ModelID { get; set; }

        public int ModelPackagingMethodOptionID { get; set; }

        public int PackagingMethodID { get; set; }

        public string PackagingMethodNM { get; set; }

        public bool IsSelected { get; set; }
    }
}