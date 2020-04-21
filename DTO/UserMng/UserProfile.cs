using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.UserMng
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string AspNetUsersId { get; set; }
        public int? UserGroupID { get; set; }
        public string UserUD { get; set; }
        public string FullName { get; set; }
        public string SkypeID { get; set; }
        public int? OfficeID { get; set; }
        public string SignatureImage { get; set; }
        public string PersonalPhoto { get; set; }
        public string DateOfBirth { get; set; }
        public bool? IsActivated { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string LastLoginDate { get; set; }
        public string UpdatorName { get; set; }
        public string CreatorName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
        [Compare("NewPassword")]
        [Display(Name = "Password Confirmation")]
        public string NewPasswordConfirmation { get; set; }

        public string PersonalPhoto_DisplayURL { get; set; }
        public bool PersonalPhoto_HasChange { get; set; }
        public string PersonalPhoto_NewFile { get; set; }

        public string SignatureImage_DisplayURL { get; set; }
        public bool SignatureImage_HasChange { get; set; }
        public string SignatureImage_NewFile { get; set; }

        public string ConcurrencyFlag_String { get; set; }
        public List<FactoryAccess> FactoryAccesses { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
        public List<UserPermission> AffectivePermissions { get; set; }
    }
}