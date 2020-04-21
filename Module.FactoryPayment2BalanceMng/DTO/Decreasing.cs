using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2BalanceMng.DTO
{
    public class Decreasing
    {
        public int? FactoryPayment2ID { get; set; }
        public string ReceiptNo { get; set; }
        public string PaymentDate { get; set; }
        public string Season { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
