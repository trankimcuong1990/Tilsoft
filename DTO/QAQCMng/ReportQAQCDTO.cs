using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QAQCMng
{
    public class ReportQAQCDTO
    {
        public int ReportQAQCID { get; set; }
        public Nullable<int> QAQCID { get; set; }
        public Nullable<int> TypeOfInspectionID { get; set; }
        public string FinishedDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ReadyQty { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string ManagementStringID { get; set; }
        public Nullable<bool> IsSynced { get; set; }
        public string SyncedDate { get; set; }
        public string ReportDate { get; set; }
        public Nullable<int> CheckedQty { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ImageName { get; set; }
        public double? LatitudeC { get; set; }
        public double? LongitudeC { get; set; }
        public double? LatitudeF { get; set; }
        public double? LongitudeF { get; set; }
        public List<ReportCheckListDTO> ReportCheckListDTOs { get; set; }
        public List<ReportDefectDTO> ReportDefectDTOs { get; set; }
    }
}
