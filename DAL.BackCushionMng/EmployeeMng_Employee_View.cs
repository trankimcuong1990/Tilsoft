//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.BackCushionMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeMng_Employee_View
    {
        public int EmployeeID { get; set; }
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
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> JobLevelID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string DailyTask { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<int> ReportToID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string TilsoftUsage { get; set; }
        public string English { get; set; }
        public Nullable<bool> IsSuperUser { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string PersonalPhoto_DisplayURL { get; set; }
        public string Location { get; set; }
        public string CompanyNM { get; set; }
        public string DepartmentNM { get; set; }
        public string JobLevel { get; set; }
        public Nullable<bool> HasLeft { get; set; }
    }
}
