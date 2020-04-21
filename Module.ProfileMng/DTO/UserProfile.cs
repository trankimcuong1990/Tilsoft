using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProfileMng.DTO
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public int? UserGroupID { get; set; }
        public string UserUD { get; set; }
        public string UserGroupNM { get; set; }
        public bool IsActivated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsSuperUser { get; set; }
        public string LastLoginDate { get; set; }
        public string LastPasswordChanged { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string EmployeeNM { get; set; }
        public string EmployeeFirstNM { get; set; }
        public string JobTitle { get; set; }
        public string JobLevelNM { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string SkypeID { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string DateOfBirth { get; set; }
        public int? LocationID { get; set; }
        public string LocationNM { get; set; }
        public string DateStart { get; set; }
        public string ManagerName { get; set; }
        public int? ReportToID { get; set; }
        public string InternalDepartmentNM { get; set; }
        public string InternalCompanyNM { get; set; }
        public string English { get; set; }
        public string OtherContactInfo { get; set; }
        public string JobDescription { get; set; }
        public string DailyTask { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string LicensedDate { get; set; }

        public int? JobLevelID { get; set; }
        public int? EmployeeID { get; set; }
        public int? CompanyID { get; set; }
        public int? DepartmentID { get; set; }

        public string NewFile { get; set; }
        public bool HasChanged { get; set; }

        public string CVFileName { get; set; }
        public string CVFileLocation { get; set; }
        public string CVNewFile { get; set; }
        public bool CVHasChanged { get; set; }
        public string APIKey { get; set; }

        public string SignatureFileName { get; set; }
        public string SignatureFileLocation { get; set; }
        public string SignatureNewFile { get; set; }
        public bool SignatureHasChanged { get; set; }

    }
}
