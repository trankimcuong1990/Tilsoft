using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Module.PODMng.DTO
{
    public class POD
    {
        public int PoDID { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "POD code must be  between 2 and 10  character")]
        [MinLength(2, ErrorMessage = "POD code must be  between 2 and 10  character")]
        public string PoDUD { set; get; }

        [Required]
        [MaxLength(255, ErrorMessage = " POD Name only accept 255 characters")]
        public string PoDName { set; get; }
        public int? ClientCountryID { get; set; }
    }
}
