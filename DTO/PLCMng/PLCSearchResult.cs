using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PLCMng
{
    public class PLCSearchResult
    {
        public int? PLCID { get; set; }
        public bool? IsConfirmed { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string CachedClientUD { get; set; }
        public string CachedContanierNo { get; set; }
        public string CachedLoadingPlanUD { get; set; }
        public string CushionBatchCode { get; set; }
        public string CachedProformaInvoiceNo { get; set; }
        public string Rating { get; set; }
        public string RatorName { get; set; }
        public int RatorID { get; set; }
        public string RatedDate { get; set; }
        public string UpdatorName { get; set; }
        public int UpdatorID { get; set; }
        public string UpdatedDate { get; set; }
        public string ConfirmerName { get; set; }
        public int ConfirmerID { get; set; }
        public string ConfirmedDate { get; set; }
        public int? FactoryID { get; set; }
        public string Season { get; set; }
        public bool IsSelected { get; set; }
    }
}