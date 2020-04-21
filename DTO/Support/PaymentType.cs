using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class PaymentType
    {
        public int? ConstantEntryID { get; set; }
        public int? PaymentTypeID { get; set; }
        public string PaymentTypeNM { get; set; }
    }
}