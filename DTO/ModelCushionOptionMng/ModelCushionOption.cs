using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelCushionOptionMng
{
    public class ModelCushionOption
    {
        public int ModelCushionOptionID { get; set; }

        [Required]
        [Display(Name = "Model ID")]
        public int ModelID { get; set; }

        [Required]
        [Display(Name = "Cushion Option ID")]
        public Nullable<int> CushionOptionID { get; set; }

        [Required]
        [Display(Name = "Updator Name")]
        public string UpdatorName { get; set; }

        [Required]
        [Display(Name = "Date Updated")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [Required]
        [Display(Name = "Confirmed")]
        public Nullable<bool> IsConfirmed { get; set; }

        [Required]
        [Display(Name = "Confirmator Name")]
        public string ConfirmatorName { get; set; }

        [Required]
        [Display(Name = "Date Confirmed")]
        public Nullable<System.DateTime> ConfirmedDate { get; set; }

        [Required]
        [Display(Name = "Cushion Name")]
        public string CushionNM { get; set; }

        [Required]
        [Display(Name = "Cushion Color Name")]
        public string CushionColorNM { get; set; }
    }
}
