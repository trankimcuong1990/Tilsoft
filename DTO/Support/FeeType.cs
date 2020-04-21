using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class FeeType
    {
        public int? ConstantEntryID { get; set; }
        public int? FeeTypeID { get; set; }
        public string FeeTypeNM { get; set; }
    }
}