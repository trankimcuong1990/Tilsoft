using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class SampleTransportType
    {
        public int TransportTypeID { get; set; }

        public string TransportTypeNM { get; set; }

        public int? DisplayOrder { get; set; }

    }
}