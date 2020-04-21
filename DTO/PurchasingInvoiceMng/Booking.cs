using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PurchasingInvoiceMng
{
    public class Booking
    {
        public int? BookingID { get; set; }

        public string BLNo { get; set; }

        public string ShipedDate { get; set; }

        public string SupplierNM { get; set; }

        public string ForwarderNM { get; set; }

        public string Feeder { get; set; }

        public string POLName { get; set; }

        public string PODName { get; set; }

        public int? SupplierID { get; set; }

    }
}