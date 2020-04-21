using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
namespace DTO.DocumentClientMng
{
    public class TypeOfDelivery
    {
        [Display(Name = "Type Of Delivery")]
        public int TypeOfDeliveryID { get; set; }
        [Display(Name = "Type Of Delivery Code")]
        public string TypeOfDeliveryUD { get; set; }
        [Display(Name = "Type Of Delivery Name")]
        public string TypeOfDeliveryNM { get; set; }
    }
}
