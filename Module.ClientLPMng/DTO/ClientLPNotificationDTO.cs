using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientLPMng.DTO
{
    public class ClientLPNotificationDTO
    {
        public int ClientLPNotificationID { get; set; }
        public int? ClientLPID { get; set; }
        public int? UserID { get; set; }
        public string EmployeeUD { get; set; }
        public string EmployeeNM { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string InternalCompanyNM { get; set; }

    }
}
