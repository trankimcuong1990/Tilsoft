using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class LoadingPlanDTO
    {
        public int LoadingPlanID { get; set; }
        public int? BookingID { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public int? ContainerTypeID { get; set; }
    }
}
