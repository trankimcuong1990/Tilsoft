using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuotationMng
{
    public class EditFormData
    {
        public Quotation Data { get; set; }
        public List<Support.PaymentTerm> PaymentTerms { get; set; }
        public List<Support.DeliveryTerm> DeliveryTerms { get; set; }
        public List<Support.OfferDirection> OfferDirections { get; set; }
        public List<Support.PriceDifference> PriceDifferences { get; set; }
        public List<Support.QuotationStatus> QuotationStatuses { get; set; }
    }
}
