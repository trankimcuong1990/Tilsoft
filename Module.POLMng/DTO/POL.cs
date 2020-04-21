using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Module.POLMng.DTO
{
   public class POL
    {
      
       public int PoLID { get; set; }
        [Required]
       [MaxLength(3, ErrorMessage = "POL must has 3 characters, only number or letter accepted")]
       [MinLength(3, ErrorMessage = "POL Code must has 3 characters, only number or letter accepted")]
       public string PoLUD { get; set; }

       [Required]
       [MaxLength(255, ErrorMessage = "POL Name only accept 255 characters")]
       public string PoLname { get; set; }
    }
}
