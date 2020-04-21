using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientMng
{
    public class ClientContact
    {
        [Display(Name = "ClientContactID")]
        public int? ClientContactID { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "Salutation")]
        public string Salutation { get; set; }

        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Display(Name = "CallingName")]
        public string CallingName { get; set; }

        [Display(Name = "JobTitle")]
        public string JobTitle { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "IsDefault")]
        public bool? IsDefault { get; set; }

        [Display(Name = "IsEurofarAgent")]
        public bool? IsEurofarAgent { get; set; }

        public string EurofarstockUserName { get; set; }
        public string EurofarstockPassword { get; set; }
        public int? DisplayIndex { get; set; }
    }
}