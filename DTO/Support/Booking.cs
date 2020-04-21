using System;

namespace DTO.Support
{
    public class Booking
    {
        public int? BookingID { get; set; }
        public string BookingUD { get; set; }
        public int? ForwarderID { get; set; }
        public string BLNo { get; set; }
        public DateTime? ETD { get; set; }
        public DateTime? ETA { get; set; }
        public string ForwarderInfo { get; set; }
        public string PoDName { get; set; }
        public string PoLname { get; set; }
    }
}