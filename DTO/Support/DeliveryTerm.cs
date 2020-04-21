using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class DeliveryTerm
    {
        [Display(Name = "DeliveryTermID")]
        public int DeliveryTermID { get; set; }

        [Display(Name = "DeliveryTermNM")]
        public string DeliveryTermNM { get; set; }

        [Display(Name = "DeliveryTypeNM")]
        public string DeliveryTypeNM { get; set; }

    }
}