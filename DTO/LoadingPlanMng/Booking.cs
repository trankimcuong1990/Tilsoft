using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LoadingPlanMng
{
    public class Booking
    {
        public int BookingID { get; set; }

        [Display(Name = "DisplayText")]
        public string DisplayText { get; set; }

        [Display(Name = "BookingUD")]
        public string BookingUD { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
    }
}