using System;

namespace Module.TransportCostForwarder.DTO
{
    public class BookingForwarder
    {
        public int BookingID { get; set; }
        public string BookingUD { get; set; }
        public int? ForwarderID { get; set; }
        public string ForwarderUD { get; set; }
        public string ForwarderNM { get; set; }
        public string ETD { get; set; }
        public int? POLID { get; set; }
        public string PoLUD { get; set; }
        public string PoLname { get; set; }
        public int? PODID { get; set; }
        public string PoDUD { get; set; }
        public string PoDName { get; set; }
        public string SettingValue { get; set; }
        public string BLNo { get; set; }
    }
}
