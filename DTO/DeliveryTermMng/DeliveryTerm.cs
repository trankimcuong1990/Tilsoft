using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DeliveryTermMng
{
    public class DeliveryTerm
    {
        public int DeliveryTermID { get; set; }

       
        [Display(Name = "Delivery Term Code")]
        public string DeliveryTermUD { get; set; }

        [Required]
        [Display(Name = "Delivery Term Name")]
        public string DeliveryTermNM { get; set; }
        
        [Required]
        [Display(Name = "IsActive?")]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "Delivery Type")]
        public string DeliveryType { get; set; }

    }
}