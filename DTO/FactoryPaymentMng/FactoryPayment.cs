using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryPaymentMng
{
    public class FactoryPayment
    {
        public int? FactoryPaymentID { get; set; }

        public string PaymentReceiptNo { get; set; }

        public string PaymentDate { get; set; }

        public string Season { get; set; }

        public int? FactoryID { get; set; }

        public decimal? USDAmount { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public byte[] ConcurrencyFlag { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public string ConcurrencyFlag_String { get; set; }

        public string CreatorName2 { get; set; }

        public string UpdatorName2 { get; set; }
    }
}