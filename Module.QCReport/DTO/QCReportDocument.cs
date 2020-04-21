using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class QCReportDocument
    {
        public int QCReportDocumentID { get; set; }
        public string QCReportDocumentUD { get; set; }
        public int? QCReportDocumentTypeID { get; set; }
        public string FileUD { get; set; }
        public string Remark { get; set; }
        public int? QCReportID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public string QCReportDocumentTypeNM { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string QCReportDocument_NewFile { get; set; }
        public bool QCReportDocument_HasChange { get; set; }

        public string UpdatorName { get; set; }
        public bool? HasLink { get; set; }
    }
}
