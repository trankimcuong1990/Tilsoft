using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO
{
    public class QAQCFactoryAccess
    {
        public int QAQCFactoryAccessID { get; set; }
        public Nullable<int> UserAccessID { get; set; }
        public Nullable<int> ApprovalID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string UserAccessNM { get; set; }
        public string ApprovalNM { get; set; }
    }
}
