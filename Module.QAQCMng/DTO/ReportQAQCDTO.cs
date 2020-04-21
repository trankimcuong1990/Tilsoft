using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class ReportQAQCDTO
    {
        public ReportQAQCDTO()
        {
            reportDefects = new List<ReportDefectDTO>();
            reportCheckLists = new List<ReportCheckListDTO>();
        }
        public int ReportQAQCID { get; set; }
        public Nullable<int> QAQCID { get; set; }
        public Nullable<int> TypeOfInspectionID { get; set; }
        public string TypeOfInspecNM { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusNM { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ReadyQty { get; set; }
        public Nullable<int> CheckedQty { get; set; }
        public string ReportDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedNM { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedNM { get; set; }
        public string UpdatedDate { get; set; }
        public string FinishedDate { get; set; }
        public Nullable<int> ApprovalBy { get; set; }
        public string ApprovalNM { get; set; }
        public String ApprovalDate { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string Comment { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ImageName { get; set; }
        public double? LatitudeC { get; set; }
        public double? LongitudeC { get; set; }
        public double? LatitudeF { get; set; }
        public double? LongitudeF { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public List<QAQCMng.DTO.ReportDefectDTO> reportDefects { get; set; }
        public List<QAQCMng.DTO.ReportCheckListDTO> reportCheckLists { get; set; }
       
    }
}
