namespace Module.TransportCICharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ClientBookingList
    {
        public List<ClientBooking> Data { get; set; }
        public int TotalRows { get; set; }
    }

    public class ClientBooking
    {
        public int BookingID { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string ETD { get; set; }
        public int? POLID { get; set; }
        public string PoLUD { get; set; }
        public string PoLname { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
    }
}
