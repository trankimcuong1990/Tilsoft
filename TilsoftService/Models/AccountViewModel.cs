using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TilsoftService.Models
{
    public class AccountViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(7, ErrorMessage="Password length must be at least 7 chars")]
        public string Password { get; set; }

        [Required]
        public int UserGroupID { get; set; }

        public int? EmployeeID { get; set; }
        public bool IsActive { get; set; }
    }
}