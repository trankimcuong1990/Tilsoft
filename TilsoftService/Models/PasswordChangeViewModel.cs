using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TilsoftService.Models
{
    public class PasswordChangeViewModel
    {
        public string UserName { get; set; }

        [Required]
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }

        public string Confirmation { get; set; }
    }
}