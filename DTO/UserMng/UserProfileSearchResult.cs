using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.UserMng
{
    public class UserProfileSearchResult
    {
        public int UserId { get; set; }
        public string UserUD { get; set; }
        public string FullName { get; set; }
        public string UserGroupNM { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SkypeID { get; set; }
        public string OfficeNM { get; set; }        
        public bool IsActivated { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string LastLoginDate { get; set; }
        public string PersonalPhoto_DisplayURL { get; set; }
    }
}