using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class UsersDTO
    {
        public int EmployeeID { get; set; }
        public int UserID { get; set; }
        public string EmployeeNM { get; set; }
        public string InternalCompanyNM { get; set; }
        public int CompanyID { get; set; }
    }
}
