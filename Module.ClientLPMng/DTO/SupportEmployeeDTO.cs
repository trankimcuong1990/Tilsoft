using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientLPMng.DTO
{
    public class SupportEmployeeDTO
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public int? CompanyID { get; set; }
        public string InternalCompanyNM { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
}
