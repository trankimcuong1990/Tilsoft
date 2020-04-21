using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.EmployeeMng.DTO
{
    public class EmployeeSearchResult
    {
        public int? EmployeeID { get; set; }
        public string EmployeeUD { get; set; }
        public string EmployeeNM { get; set; }
        public string EmployeeFirstNM { get; set; }
        public string Phonetic { get; set; }
        public string SkypeID { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string OtherContactInfo { get; set; }
        public string PersonalPhoto_DisplayURL { get; set; }
        public string InternalCompanyNM { get; set; }
        public string InternalDepartmentNM { get; set; }
        public string JobLevelNM { get; set; }
        public string LocationNM { get; set; }

        //IDs
        public int? InternalCompanyID { get; set; }
        public int? InternalDepartmentID { get; set; }
        public int? JobLeveID { get; set; }
        public int? LocationID { get; set; }
        //
        public string JobTitle { get; set; }
        public object JobDescription { get; set; }
        public string DailyTask { get; set; }
        public bool? Gender { get; set; }
        public string ReporterName { get; set; }
        public bool? IsActivated { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Age { get; set; }
        public string JoinedYear { get; set; }
        public string DateOfBirth { get; set; }
        public string TilsoftUsage { get; set; }
        public int? TilsoftUsageID { get; set; }
        public string English { get; set; }
        public bool? IsSuperUser { get; set; }
        public bool HasLeft { get; set; }
        public int? UserID { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }

        public int? CompanyID { get; set; }
        public string CompanyUD { get; set; }
        public string CompanyNM { get; set; }
        public string ShortName { get; set; }

        public int? BranchID { get; set; }
        public string BranchUD { get; set; }
        public string BranchNM { get; set; }
        public int? ManagerID { get; set; }
    }
}