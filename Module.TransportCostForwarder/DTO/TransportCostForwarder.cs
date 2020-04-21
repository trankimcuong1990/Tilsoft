using System;
using System.Collections.Generic;

namespace Module.TransportCostForwarder.DTO
{
    public class TransportCostForwarder
    {
        public int TransportCostForwarderID { get; set; }
        public int? ForwarderID { get; set; }
        public string ForwarderNM { get; set; }
        public string TransportInvoiceNumber { get; set; }
        public string TransportInvoiceDate { get; set; }
        public int? BookingID { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Currency { get; set; }
        public string Authorisation { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string CreatorNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }

        public string ETD { get; set; }
        public int? PODID { get; set; }
        public int? POLID { get; set; }

        public string ForwarderUD { get; set; }

        public string CreatorEmpNM { get; set; }
        public string UpdatorEmpNM { get; set; }


        public List<TransportCostForwarderItem> TransportCostForwarderItems { get; set; }
    }
}
