using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ProfileMng
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserUD { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string SkypeID { get; set; }
        public string DateOfBirth { get; set; }
        public string OfficeNM { get; set; }
        public string UserGroupNM { get; set; }

        [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
        [Compare("NewPassword")]
        [Display(Name = "Password Confirmation")]
        public string NewPasswordConfirmation { get; set; }

        //
        // signature image
        //
        public string SignatureImage { get; set; }
        public bool SignatureImage_HasChange { get; set; }
        public string SignatureImage_NewFile { get; set; }
        public string SignatureImage_DisplayUrl { get; set; }

        //
        // personal photo
        //
        public string PersonalPhoto { get; set; }
        public bool PersonalPhoto_HasChange { get; set; }
        public string PersonalPhoto_NewFile { get; set; }
        public string PersonalPhoto_DisplayUrl { get; set; }

        //
        // concurrency
        //
        public string ConcurrencyFlag_String { get; set; }
    }
}