using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class QCReportDTO
    {
        public int QCReportID { get; set; }
        public string QCReportUD { get; set; }
        public string InspectedDate { get; set; }
        public int? FactoryID { get; set; }
        public string SampleSize { get; set; }
        public string Location { get; set; }
        public string AssemplySampleSize { get; set; }
        public string QCName { get; set; }
        public int? SaleOrderDetailID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public int? Quantity { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? InspectedPackedQuantity { get; set; }
        public int? InspectedNotPackedQuantity { get; set; }
        public int? InspectedPackedCartonQuantity { get; set; }
        public int? InspectedNotPackedCartonQuantity { get; set; }
        public int? InspectionStageID { get; set; }
        public string InspectionStageNM { get; set; }
        public decimal? AQLCritical { get; set; }
        public decimal? AQLMajor { get; set; }
        public decimal? AQLMinor { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdaterName { get; set; }
        public string FullName { get; set; }
        public int? PackagingMethodID { get; set; }
        public string PackagingMethodNM { get; set; }
        public bool? IsConformed { get; set; }

        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }

        public bool ScanHasChange { get; set; }
        public string ScanNewFile { get; set; }
        public string ThumbnailLocation { get; set; }
        public string ProductImage { get; set; }

        public int? InspectionResultID { get; set; }
        public string InspectionResultNM { get; set; }
        public string BeginInspection { get; set; }
        public string EndInspection { get; set; }
        public decimal? MaximumCritical { get; set; }
        public decimal? MaximumMajor { get; set; }
        public decimal? MaximumMinor { get; set; }
        public string ReportFile { get; set; }
        public string ReportFileLocation { get; set; }
        public string ReportFriendlyName { get; set; }
        public bool ReportScanHasChange { get; set; }
        public string ReportScanNewFile { get; set; }

        public List<DTO.QCReportSummaryDTO> QCReportSummaryDTOs { get; set; }
        public List<DTO.QCReportDetailDTO> QCReportDetailDTOs { get; set; }
        public List<DTO.QCReportDefectDTO> QCReportDefectDTOs { get; set; }
        public List<DTO.QCReportImageDTO> QCReportImageDTOs { get; set; }
        public List<DTO.QCReportTestEnvironmentDTO> QCReportTestEnvironmentDTOs { get; set; }
        public List<DTO.QCReportFactoryOrderDetailDTO> QCReportFactoryOrderDetailDTOs { get; set; }
    }
}
