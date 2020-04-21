using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class QCReport
    {
        public int? QCReportID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public string QCReportNM { get; set; }
        public string InspectedDay { get; set; }
        public string Location { get; set; }
        public int InspectionTypeID { get; set; }
        public string SampleSize { get; set; }
        public string AssemplySampleSize { get; set; }
        public int PackagingTypeID { get; set; }
        public string RejectedReason { get; set; }
        public string CorrectiveAction { get; set; }
        public string Remark { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public int OrderQnt { get; set; }
        public string QAName { get; set; }
        public string QCName { get; set; }
        public bool? IsPassed { get; set; }
        public bool? IsRejected { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string FileUD { get; set; }
        public string AttachedFile_NewFile { get; set; }
        public bool AttachedFile_HasChanged { get; set; }

        public List<QCReportImage> QCReportImages { get; set; }
        public List<QCReportDetail> QCReportDetails { get; set; }
        public List<QCReportDefect> QCReportDefects { get; set; }
        public List<DTO.QCReportDocument> QCReportDocuments { get; set; } // qc report document files | tran.cuong | 20180227
    }
}
