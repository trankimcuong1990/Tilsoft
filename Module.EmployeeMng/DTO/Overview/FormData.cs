using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO.Overview
{
    public class FormData
    {
        public Overview.EmployeeDTO Data { get; set; }
        public Overview.ManagementData SensitiveData { get; set; }
        public List<Overview.UserDataRpt> UserDataRpt { get; set; }
        public List<Overview.HitCountDataRpt> HitCountDataRpt { get; set; }
    }
}
