using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PLCMng
{
    public class PLC
    {
        public int PLCID { get; set; }
        public int? BookingID { get; set; }
        public int? FactoryID { get; set; }
        public int? OfferLineID { get; set; }
        public int? OfferLineSparepartID { get; set; }
        public string CushionBatchCode { get; set; }
        public string QCReportFile { get; set; }
        public string AssemplyInstructionFile { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
        public string RatingComment { get; set; }
        public int? RatedBy { get; set; }
        public string RatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public string CachedClientUD { get; set; }
        public string CachedContanierNo { get; set; }
        public string CachedLoadingPlanUD { get; set; }
        public string CachedProformaInvoiceNo { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string RatorName { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }

        public bool isRated { get; set; }

        public string ConcurrencyFlag_String { get; set; }
        public List<PLCImage> PLCImages { get; set; }
    }
}