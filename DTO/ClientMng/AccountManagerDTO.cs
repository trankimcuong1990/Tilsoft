using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class AccountManagerDTO
    {
        public int UserID { get; set; }
        public string SaleUD { get; set; }
        public string EmployeeFirstNM { get; set; }
        public string EmployeeNM { get; set; }
        public bool IsAccountManager { get; set; }
        public bool IsAccountManagerAssistant { get; set; }
        public bool IsVNAssistant { get; set; }
        public bool IsVNLogisticAssistant { get; set; }
    }
}
