using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class ContainerTransport
    {
        public int? LoadingPlanID { get; set; }
        public int? BookingID { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public int? ContainerTypeID { get; set; }
        public string ETD { get; set; }

    }
}
