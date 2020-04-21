using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AgendaMng.DTO
{
    public class AgendaMngUser
    {
        public int ManagerUserID { get; set; }
        public int? AppointmentID { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public bool PIC { get; set; }
        public bool IsReceivePriceRequest { get; set; }

    }
}
