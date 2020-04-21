using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.QuotationMng
{
    public class Quotation
    {
        public int QuotationID { get; set; }

        [StringLength(17, MinimumLength=1, ErrorMessage="Quotation No must be between 1 and 17 chars")]
        public string QuotationUD { get; set; }
        public int? FactoryID { get; set; }
        public string Season { get; set; }
        public int? DeliveryTermID { get; set; }
        public int? PaymentTermID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string UpdatorName { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }

        public List<QuotationDetail> QuotationDetails { get; set; }
        public List<QuotationOffer> QuotationOffers { get; set; }

        public string UpdatorName2 { get; set; }
    }
}