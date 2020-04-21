using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotationMng.DTO
{
    public class Quotation
    {
        public int QuotationID { get; set; }
        public string Season { get; set; }
        public string QuotationUD { get; set; }
        public string FactoryUD { get; set; }
        public string PaymentTermNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public int? FactoryID { get; set; }
        public string UpdatedDate { get; set; }

        public List<QuotationDetail> QuotationDetails { get; set; }
    }
}
