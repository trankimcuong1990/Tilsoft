﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionTestingMng.DTO
{
    public class CushionTestFileSearchDTO
    {
        public int CushionTestReportFileID { get; set; }
        public int? CushionTestReportID { get; set; }
        public string Remark { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
    }
}
