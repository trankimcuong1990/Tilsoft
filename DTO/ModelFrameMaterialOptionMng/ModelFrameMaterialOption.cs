using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelFrameMaterialOptionMng
{
    public class ModelFrameMaterialOption
    {
        public int ModelFrameMaterialOptionID { get; set; }

        [Required]
        [Display(Name = "Model ID")]
        public int ModelID { get; set; }

        [Required]
        [Display(Name = "Frame Material ID")]
        public int FrameMaterialID { get; set; }

    }
}