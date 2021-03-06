﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AccountMng
{
    public class User
    {
        public int UserId { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserUD { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SkypeID { get; set; }
        public int OfficeID { get; set; }
        public string SignatureImage { get; set; }
        public string PersonalPhoto { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SignatureImage_DisplayUrl { get; set; }
        public string PersonalPhoto_DisplayUrl { get; set; }

        public int UserGroupID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int FactoryID { get; set; }
        public string BranchUD { get; set; }

        public List<UserPermission> Permissions { get; set; }
        public List<ModuleSpecialPermissionAccess> ModuleSpecialPermissionAccesses { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
