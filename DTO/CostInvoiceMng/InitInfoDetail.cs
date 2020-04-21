using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CostInvoiceMng
{
    public class InitInfoDetail
    {
        public object KeyID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public int? BookingID { get; set; }

        public int? ClientID { get; set; }

        public int? LoadingPlanID { get; set; }

        public string ContainerNo { get; set; }

        public bool? SeaFreightInvoiced { get; set; }
        public bool? TruckingInvoiced { get; set; }

    }
}