using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
namespace DTO.DocumentClientMng
{
    public class PaymentStatus
    {
        [Display(Name = "Payment Status")]
        public int PaymentStatusID { get; set; }
        [Display(Name = "Payment Status Code")]
        public string PaymentStatusUD { get; set; }
        [Display(Name = "Payment Status Name")]
        public string PaymentStatusNM { get; set; }
    }
}
