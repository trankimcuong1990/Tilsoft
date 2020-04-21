﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class QCReportDetail
    {
        public int? QCReportDetailID { get; set; }
        public int? QCReportID { get; set; }
        public int RowIndex { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public string Actual { get; set; }
    }
}
