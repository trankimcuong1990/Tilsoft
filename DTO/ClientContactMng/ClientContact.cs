using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientContactMng
{
    public class ClientContact
    {
        public int ClientContactID { get; set; }

        [Required]
        [Display(Name = "Client ID")]
        public Nullable<int> ClientID { get; set; }

        [Required]
        [Display(Name = "Salutation")]
        public string Salutation { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Calling Name")]
        public string CallingName { get; set; }

        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required]
        [Display(Name = "Moblie Phone Number")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Required]
        [Display(Name = "Updator Name")]
        public string UpdatorName { get; set; }

        [Required]
        [Display(Name = "Date Updated")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
