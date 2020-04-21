using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class PurchasingCalendarUserDTO
    {
        public int PurchasingCalendarUserID { get; set; }
        public Nullable<int> PurchasingCalendarAppointmentID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<bool> PIC { get; set; }
        public Nullable<bool> IsReceivePriceRequest { get; set; }
    }
}
