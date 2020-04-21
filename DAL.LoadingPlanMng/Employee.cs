//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.LoadingPlanMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeUD { get; set; }
        public string EmployeeNM { get; set; }
        public string SkypeID { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string OtherContactInfo { get; set; }
        public string PersonalPhoto { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> JobLevelID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public Nullable<int> ReportToID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string DailyTask { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string EmployeeFirstNM { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> Age { get; set; }
        public string TilsoftUsage { get; set; }
        public string English { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<bool> IsSuperUser { get; set; }
        public string Phonetic { get; set; }
        public Nullable<bool> HasLeft { get; set; }
        public string ResumeFile { get; set; }
        public string ManagerNote { get; set; }
        public Nullable<System.DateTime> LeftDate { get; set; }
        public Nullable<System.DateTime> ContractPeriod { get; set; }
        public Nullable<int> NumberOfContractMonths { get; set; }
        public string TypeOfContract { get; set; }
        public Nullable<System.DateTime> ContractStartDate { get; set; }
        public Nullable<bool> IsAccountManager { get; set; }
        public Nullable<bool> IsAccountManagerAssistant { get; set; }
        public string SaleUD { get; set; }
        public Nullable<int> AccountManagerTypeID { get; set; }
        public Nullable<bool> IsVNAssistant { get; set; }
        public Nullable<bool> IsVNLogisticAssistant { get; set; }
        public Nullable<bool> IsIncludeInDDC { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<bool> IsSCMResponsible { get; set; }
        public string DefaultColor { get; set; }
        public string SignatureFile { get; set; }
    }
}
