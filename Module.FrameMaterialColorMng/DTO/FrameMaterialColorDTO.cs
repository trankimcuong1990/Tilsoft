using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FrameMaterialColorMng.DTO
{
   public class FrameMaterialColorDTO
    {

        public int FrameMaterialColorID { get; set; }        
        public string FrameMaterialColorUD { get; set; }
        [Required(ErrorMessage = "Name field is required")]
        [StringLength(255, ErrorMessage = "The Name value cannot exceed 255 characters. ")]
        public string FrameMaterialColorNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdateName { get; set; }
        public int Total { get; set; }
    }
}
