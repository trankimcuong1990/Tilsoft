using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientPaymentMng
{
    public class ClientPayment
    {
        public int? ClientPaymentID { get; set; }

        public int? SaleOrderID { get; set; }

        public string PaymentDate { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public byte[] ConcurrencyFlag { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string SaleNM { get; set; }

        public decimal? OrderAmount { get; set; }

        public decimal? PaymentAmount { get; set; }

        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }

        public string ConcurrencyFlag_String { get; set; }
        public List<ClientPaymentDetail> ClientPaymentDetails { get; set; }

        public decimal? OrderDownAmount { get; set; }
        public bool IsLCReceived { get; set; }
        public string PaymentTermNM { get; set; }
        public string PaymentTypeNM { get; set; }

    }
}