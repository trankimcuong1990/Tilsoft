using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
namespace DTO.DocumentClientMng
{
    public class DeliveryStatus
    {
        [Display(Name = "Delivery Status")]
        public int DeliveryStatusID { get; set; }
        [Display(Name = "Delivery Status Code")]
        public string DeliveryStatusUD { get; set; }
        [Display(Name = "Delivery Status Name")]
        public string DeliveryStatusNM { get; set; }
    }
}
