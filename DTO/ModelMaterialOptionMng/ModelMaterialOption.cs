using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelMaterialOptionMng
{
    public class ModelMaterialOption
    {
        public int ModelMaterialOptionID { get; set; }

        [Required]
        [Display(Name = "Model ID")]
        public int ModelID { get; set; }

        [Required]
        [Display(Name = "Material Option ID")]
        public int MaterialOptionID { get; set; }

    }
}