using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class PaymentTerm
    {
        [Display(Name = "PaymentTermID")]
        public int? PaymentTermID { get; set; }

        [Display(Name = "PaymentTermNM")]
        public string PaymentTermNM { get; set; }

        [Display(Name = "PaymentTypeNM")]
        public string PaymentTypeNM { get; set; }

        public int? PaymentTypeID { get; set; }
        public int? InDays { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? DownValue { get; set; }

    }
}