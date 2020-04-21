using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelPackagingMethodOptionMng
{
    public class ModelPackagingMethodOption
    {
        public int ModelPackagingMethodOptionID { get; set; }

        [Required]
        [Display(Name = "Model ID")]
        public int ModelID { get; set; }

        [Required]
        [Display(Name = "Packaging Method ID")]
        public Nullable<int> PackagingMethodID { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Updator Name")]
        public string UpdatorName { get; set; }

        [Required]
        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [Required]
        [Display(Name = "Is Confirmed")]
        public Nullable<bool> IsConfirmed { get; set; }

        [Required]
        [Display(Name = "Confirmator Name")]
        public string ConfirmatorName { get; set; }

        [Required]
        [Display(Name = "Confirmed Date")]
        public Nullable<System.DateTime> ConfirmedDate { get; set; }

        [Required]
        [Display(Name = "Packaging Method Code")]
        public string PackagingMethodUD { get; set; }

        [Required]
        [Display(Name = "Packaging Method Name")]
        public string PackagingMethodNM { get; set; }
    }
}
