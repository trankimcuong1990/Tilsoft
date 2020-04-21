using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.TransportationFeeMng
{
    public class TransportationFee
    {
        [Required]
        [MaxLength(9, ErrorMessage = "Season should contain no more than 9 characters.")]
        [MinLength(9, ErrorMessage = "Season should contain no less than 9 characters.")]
        public string Season { get; set; }

        [Required]
        public int Transportation { get; set; }

        [Required]
        public int Commission { get; set; }
    }
}
