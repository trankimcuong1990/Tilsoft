using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.QuotationMng
{
    public class QuotationDetail
    {
        public int QuotationDetailID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? PriceDifferenceRate { get; set; }
        public decimal? BreakDownPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TargetPrice { get; set; }
        public string Remark { get; set; }
        public int? StatusID { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string QuotationStatusNM { get; set; }
        public string UpdatorName { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string LDS { get; set; }
        public string ClientUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public int OrderQnt { get; set; }
        public object DBObject { get; set; }
        public string UpdatorName2 { get; set; }
    }
}