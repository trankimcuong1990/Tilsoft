using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ConsigneeType
    {
        public int ConsigneeTypeID { get; set; }

        public string ConsigneeTypeNM { get; set; }

        public int? DisplayOrder { get; set; }

    }
}