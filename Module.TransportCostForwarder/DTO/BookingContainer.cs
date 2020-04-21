namespace Module.TransportCostForwarder.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BookingContainer
    {
        public int BookingID { get; set; }
        public string BLNo { get; set; }
        public string ContainerNo { get; set; }
        public int? ContainerTypeID { get; set; }
    }
}
