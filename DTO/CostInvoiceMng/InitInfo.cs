using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CostInvoiceMng
{
    public class InitInfo
    {
        public object KeyID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public int? BookingID { get; set; }

        public int? ClientID { get; set; }

        public string InvoiceNo { get; set; }

        public string BLNo { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }



    }
}