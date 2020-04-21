using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.BookingMng
{
    public class LoadingPlan
    {
        public int LoadingPlanID { get; set; }

        [Display(Name = "BookingID")]
        public int? BookingID { get; set; }

        [Display(Name = "ContainerNo")]
        public string ContainerNo { get; set; }

        [Display(Name = "ContainerTypeNM")]
        public string ContainerTypeNM { get; set; }

        [Display(Name = "SealNo")]
        public string SealNo { get; set; }

    }
}