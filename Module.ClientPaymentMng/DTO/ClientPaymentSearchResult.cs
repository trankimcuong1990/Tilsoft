using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng.DTO
{
    public class ClientPaymentSearchResult
    {
        public int ClientPaymentID { get; set; }
        public bool? IsConfirmed { get; set; }
        public string ClientPaymentUD { get; set; }
        public string ClientPaymentMethodNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string TransactionNo { get; set; }
        public string LCNo { get; set; }
        public string PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public string ClientPaymentBallanceUD { get; set; }
        public string BallanceDate { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public string Remark { get; set; }

        public int UpdatedBy { get; set; }
        public string UpdatorName2 { get; set; }
        public int ConfirmedBy { get; set; }
        public string ConfirmerName2 { get; set; }
    }
}
