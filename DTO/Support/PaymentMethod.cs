using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class PaymentMethod
    {
        public int? ConstantEntryID { get; set; }
        public int? PaymentMethodID { get; set; }
        public string PaymentMethodNM { get; set; }
    }
}