using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class SampleRequestType
    {
        public int RequestTypeID { get; set; }

        public string RequestTypeNM { get; set; }

        public int? DisplayOrder { get; set; }

    }
}