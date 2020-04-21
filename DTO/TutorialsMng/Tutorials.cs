using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.Tutorials
{
    public class Tutorials
    {
       
        public int ID { get; set; }


        [Required]
        [MaxLength(255, ErrorMessage = "Tutorial Name must has 255 characters, only number or letter accepted")]
        [MinLength(2, ErrorMessage = "Tutorial Name must has 2 characters, only number or letter accepted")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Tutorial URL must has 500 characters, only number or letter accepted")]
        [MinLength(2, ErrorMessage = "Tutorial URL must has 2 characters, only number or letter accepted")]
        public string URL { get; set; }

        
       
    }
}
