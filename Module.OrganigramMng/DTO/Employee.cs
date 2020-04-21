using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrganigramMng.DTO
{
    public class Employee
    {
            public int? EmployeeID { get; set; }
            public string EmployeeNM { get; set; }
            public string SkypeID { get; set; }
            public string Telephone1 { get; set; }
            public string Email1 { get; set; }
            public string JobTitle { get; set; }
            public object JobDescription { get; set; }
            public int? UserID { get; set; }
            public string DateStart { get; set; }
            public string DateOfBirth { get; set; }
            public object DailyTask { get; set; }
            public int? CompanyID { get; set; }
            public int? DepartmentID { get; set; }
            public int? JobLevelID { get; set; }
            public int? ReportToID { get; set; }
            public string ReportToNM { get; set; }
            public string InternalCompanyNM { get; set; }
            public string InternalDepartmentNM { get; set; }
            public string JobLevelNM { get; set; }
            public string PersonalFileUrl { get; set; }
    }
}
