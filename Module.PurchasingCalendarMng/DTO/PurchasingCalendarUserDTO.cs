using System;

namespace Module.PurchasingCalendarMng.DTO
{
    public class PurchasingCalendarUserDTO
    {
        public int PurchasingCalendarUserID { get; set; }
        public Nullable<int> PurchasingCalendarAppointmentID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<bool> PIC { get; set; }
        public Nullable<bool> IsReceivePriceRequest { get; set; }
    }
}
