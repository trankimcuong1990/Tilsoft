﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class SupportSummaryResult
    {
        public int ConstantEntryID { get; set; }
        public int? QCReportSummaryResultID { get; set; }
        public string QCReportSummaryResultNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
