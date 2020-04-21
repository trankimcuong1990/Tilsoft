using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO.Overview
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string EmployeeNM { get; set; }
        public string EmployeeFirstNM { get; set; }
        public string Phonetic { get; set; }
        public string DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string LocationNM { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string SkypeID { get; set; }
        public string OtherContactInfo { get; set; }
        public string InternalCompanyNM { get; set; }
        public string JobTitle { get; set; }
        public string JobLevelNM { get; set; }
        public string InternalDepartmentNM { get; set; }
        public int? ManagerID { get; set; }
        public string ManagerName { get; set; }
        public string DateStart { get; set; }
        public string English { get; set; }
        public int? TotalWorkingYear { get; set; }
        public int? TotalWorkingMonth { get; set; }
        public bool? IsSuperUser { get; set; }
        public string JobDescription { get; set; }
        public string DailyTask { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string ResumeFileFriendlyName { get; set; }
        public string ResumeFileLocation { get; set; }
        public int CompanyID { get; set; }
        public string LicensedDate { get; set; }
        public string TilsoftUsage { get; set; }

        public List<Overview.UserDetailDTO> UserDetailDTOs { get; set; }
        public List<Overview.UserDetailWeeklyDetailDTO> UserDetailWeeklyDetailDTOs { get; set; }
        public List<Overview.UserDetailWeeklyOverviewDTO> UserDetailWeeklyOverviewDTOs { get; set; }
        public List<Overview.EmployeeFactoryDTO> EmployeeFactoryDTOs { get; set; }

        public string CompanyUD { get; set; }
        public string CompanyNM { get; set; }
        public string ShortName { get; set; }

        public int? BranchID { get; set; }
        public string BranchUD { get; set; }
        public string BranchNM { get; set; }
    }
}
