using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class SampleRemarkStatus
    {
        public int StatusID { get; set; }

        public string StatusNM { get; set; }

        public int? DisplayOrder { get; set; }

    }
}