using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng.DTO
{
    public class Users
    {
        public int PrimaryID { get; set; }
        public string EmployeeNM { get; set; }
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string EmployeeEmail { get; set; }
        public int? CompanyID { get; set; }
    }
}
