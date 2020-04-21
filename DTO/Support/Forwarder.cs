using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class Forwarder
    {
        [Display(Name = "ForwarderID")]
        public int? ForwarderID { get; set; }

        [Display(Name = "ForwarderUD")]
        public string ForwarderUD { get; set; }

        [Display(Name = "ForwarderNM")]
        public string ForwarderNM { get; set; }

        [Display(Name = "PIC")]
        public string PIC { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Address { get; set; }

    }
}