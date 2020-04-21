using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class AccountManagerDTO
    {
        public long PrimaryID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string SaleUD { get; set; }
        public string EmployeeFirstNM { get; set; }
        public string EmployeeNM { get; set; }
        public bool IsAccountManager { get; set; }
        public bool IsAccountManagerAssistant { get; set; }
        public bool IsVNAssistant { get; set; }
        public bool IsVNLogisticAssistant { get; set; }
        public bool IsIncludeInDDC { get; set; }
    }
}
