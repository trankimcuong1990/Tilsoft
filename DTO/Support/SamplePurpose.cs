using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class SamplePurpose
    {

        public int PurposeID { get; set; }

        public string PurposeNM { get; set; }

        public int? DisplayOrder { get; set; }

    }
}