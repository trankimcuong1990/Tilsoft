using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DTO
{
    public class CostInvoice2Client
    {
        public int CostInvoice2ClientID { get; set; }
        public int? CostInvoice2ID { get; set; }
        public int? ClientID { get; set; }
        public decimal? AmountClient { get; set; }
        public string ClientUD { get; set; }
    }
}
