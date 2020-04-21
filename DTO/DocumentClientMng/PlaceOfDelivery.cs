using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
namespace DTO.DocumentClientMng
{
    public class PlaceOfDelivery
    {
        [Display(Name = "Place Of Delivery")]
        public int PlaceOfDeliveryID { get; set; }
        [Display(Name = "Place Of Delivery Code")]
        public string PlaceOfDeliveryUD { get; set; }
        [Display(Name = "Place Of Delivery Name")]
        public string PlaceOfDeliveryNM { get; set; }
    }
}
