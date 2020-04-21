using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class SCMAppointmentUserDTO
    {
        public int SCMAppointmentUserID { get; set; }
        public int? SCMAppointmentID { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public bool PIC { get; set; }
        public bool IsReceivePriceRequest { get; set; }

    }
}
