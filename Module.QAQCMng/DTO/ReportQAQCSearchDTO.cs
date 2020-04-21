using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class ReportQAQCSearchDTO
    {
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
    }
}
