using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.QuotationMng
{
    public class QuotationOffer
    {
        public int QuotationOfferID { get; set; }
        public int? QuotationOfferVersion { get; set; }
        public int? QuotationOfferDirectionID { get; set; }
        public string QuotationOfferDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string QuotationOfferDirectionNM { get; set; }
        public string UpdatorName { get; set; }

        public List<QuotationOfferDetail> QuotationOfferDetails { get; set; }
        public string UpdatorName2 { get; set; }
    }
}