using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrganigramMng.DTO
{
    public class CompanyDirector
    {
        public int? EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public string SkypeID { get; set; }
        public string Telephone1 { get; set; }
        public string Email1 { get; set; }
        public string JobTitle { get; set; }
        public int? CompanyID { get; set; }
        public string InternalCompanyNM { get; set; }
        public string InternalDepartmentNM { get; set; }
        public string JobLevelNM { get; set; }
    }
}
