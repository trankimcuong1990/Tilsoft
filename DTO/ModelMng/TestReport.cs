using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelMng
{
    public class TestReport
    {
        public int? TestReportID { get; set; }
        public int? ModelID { get; set; }
        public string ScanFile { get; set; }
        public string Lab { get; set; }
        public string TestMethod { get; set; }
        public string TestDate { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string FriendlyName { get; set; }
        public string ScanFileLocation { get; set; }
        public bool ScanHasChange { get; set; }
        public string ScanNewFile { get; set; }
    }
}
