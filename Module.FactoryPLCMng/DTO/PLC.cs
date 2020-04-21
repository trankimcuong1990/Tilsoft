using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng.DTO
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
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string ContainerNo { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string RatorName { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }

        public List<PLCImage> PLCImages { get; set; }
        public string UpdatorName2 { get; set; }
        public int? PKGS { get; set; }
        public decimal? NetWeight { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? CBMS { get; set; }
        public string PackagingMethodNM { get; set; }
        public string CTNS { get; set; }
        public string HSCode { get; set; }
        public int? OfferLineSampleProductID { get; set; }
    }
}
