using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientPaymentMng
{
    public class ClientPaymentDetail
    {
        public int? ClientPaymentDetailID { get; set; }

        public int? ClientPaymentID { get; set; }

        public int? PaymentIndex { get; set; }

        public decimal? Amount { get; set; }

        public string PaymentDate { get; set; }

        public int? ReceivedBy { get; set; }

        public string ReceivedDate { get; set; }

        public string Remark { get; set; }

        public string ReceiverName { get; set; }

    }
}