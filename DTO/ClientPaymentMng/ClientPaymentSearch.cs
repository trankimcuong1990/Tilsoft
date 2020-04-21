using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientPaymentMng
{
    public class ClientPaymentSearch
    {
        public int? ClientPaymentID { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string SaleNM { get; set; }

        public int? SaleID { get; set; }

        public string PaymentDate { get; set; }

        public decimal? OrderAmount { get; set; }

        public decimal? PaymentAmount { get; set; }

        public string CreatorName { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDate { get; set; }

        public decimal? OrderDownAmount { get; set; }
        public bool IsLCReceived { get; set; }
        public string PaymentTermNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public string Season { get; set; }

    }
}