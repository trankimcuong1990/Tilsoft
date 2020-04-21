using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.QuotationMng
{
    public class QuotationOfferDetail
    {
        public int QuotationOfferDetailID { get; set; }
        public int? QuotationDetailID { get; set; }
        public decimal? Price { get; set; }
        public string Remark { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string PackagingMethodNM { get; set; }
        public string LDS { get; set; }
        public string ClientUD { get; set; }
        public string FactoryOrderUD { get; set; }
    }
}