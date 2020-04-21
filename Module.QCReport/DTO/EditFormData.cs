using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class EditFormData
    {
        public DTO.QCReport Data { get; set; }
        public List<DTO.QCReport> AvailableReports { get; set; }
        public List<DTO.QCReportImageTitleDTO> QCReportImageTitles { get; set; }
        public List<Support.DTO.InspectionType> InspectionTypes { get; set; }
        public List<Support.DTO.PackagingType> PackagingTypes { get; set; }
        public List<DTO.QCReportDocumentType> QCReportDocumentTypes { get; set; } // qc report document type | tran.cuong | 20180228
    }
}
