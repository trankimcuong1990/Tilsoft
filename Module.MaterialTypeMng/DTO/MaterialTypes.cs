using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Module.MaterialTypeMng.DTO
{
    public class MaterialTypes
    {
        public int MaterialTypeID { get; set; }

        //[Required]
        [Display(Name = "Material Type Code")]
        public string MaterialTypeUD { get; set; }

        [Required]
        [Display(Name = "Material Type Name")]
        public string MaterialTypeNM { get; set; }

        public string HangTagFile { get; set; }
        public string HangTagFileUrl { get; set; }
        public string HangTagFileFriendlyName { get; set; }
        public bool? HangTagFileHasChange { get; set; }
        public string NewHangTagFile { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdateName { get; set; }
        public int Total { get; set; }
    }
}
