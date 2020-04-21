using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.EmployeeMng.DTO
{
    public class Employee
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
        public string PersonalPhoto { get; set; }
        public int? CompanyID { get; set; }
        public int? DepartmentID { get; set; }
        public int? JobLevelID { get; set; }
        public string JobTitle { get; set; }
        public object JobDescription { get; set; }
        public string DailyTask { get; set; }
        public bool? Gender { get; set; }
        public int? ReportToID { get; set; }
        public int? UserID { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string PersonalPhoto_DisplayURL { get; set; }
        public int? LocationID { get; set; }
        public string Age { get; set; }
        public string JoinedYear { get; set; }
        public string DateOfBirth { get; set; }
        public string TilsoftUsage { get; set; }
        public int? TilsoftUsageID { get; set; }
        public string English { get; set; }
        public bool? IsSuperUser { get; set; }
        public bool? HasLeft { get; set; }
        public string LeftDate { get; set; }
        public string ContractPeriod { get; set; }
        public int? NumberOfContractMonths { get; set; }
        public string TypeOfContract { get; set; }
        public string ContractStartDate { get; set; }

        // Image
        public bool PersonalPhoto_HasChange { get; set; }
        public string PersonalPhoto_NewFile { get; set; }

        //ResumeFile
        public string ResumeFile { get; set; }
        public string ResumeFileLocation { get; set; }
        public string ResumeFileFriendlyName { get; set; }
        public bool ResumeFile_HasChange { get; set; }
        public string ResumeFile_NewFile { get; set; }
        //
        // concurrency
        //
        public string ConcurrencyFlag_String { get; set; }
        public List<AnnualLeaveSetting> AnnualLeaveSettings { get; set; }
        public List<EmployeeFactory> EmployeeFactorys { get; set; }

        public string Location { get; set; }
        public string CompanyNM { get; set; }
        public string DepartmentNM { get; set; }
        public string JobLevel { get; set; }

        // Check to view CV
        public bool IncludedResume { get; set; }

        public string ManagerNote { get; set; }
        public bool NeedToUpdateManagerData { get; set; }

        public List<EmployeeFileDTO> EmployeeFileDTOs { get; set; }
        public List<EmployeeContractDTO> EmployeeContractDTOs { get; set; }
        public List<UserDetail> UserDetails { get; set; }
        public List<UserDetailWeeklyDetail> UserDetailWeeklyDetails { get; set; }
        public List<UserDetailWeeklyOverview> UserDetailWeeklyOverviews { get; set; }
        public List<TypeOfContractDTO> typeOfContractDTOs { get; set; }

        public string SaleUD { get; set; }
        public bool? IsAccountManager { get; set; }
        public int? AccountManagerTypeID { get; set; }
        public string AccountManagerTypeNM { get; set; }
        public bool? IsAccountManagerAssistant { get; set; }
        public bool? IsVNAssistant { get; set; }
        public bool? IsVNLogisticAssistant { get; set; }
        public bool? IsIncludeInDDC { get; set; }
        public bool? IsSCMResponsible { get; set; }

        public string CompanyUD { get; set; }
        public string ShortName { get; set; }

        public int? BranchID { get; set; }
        public string BranchUD { get; set; }
        public string BranchNM { get; set; }
        public List<EmployeeResponsibleForDTO> EmployeeResponsibleForDTOs { get; set; }
        public List<QAQCFactoryAccess> factoryAccesses { get; set; }
    }
}
