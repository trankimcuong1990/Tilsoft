using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Module.CushionColorMng.DTO
{
    public class CushionColorSearchResult
    {
        public int CushionColorID { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        // Test 1
        public string FriendlyName1 { get; set; }
        public string FileLocation1 { get; set; }

        // Test 2
        public string FriendlyName2 { get; set; }
        public string FileLocation2 { get; set; }

        // Test 3
        public string FriendlyName3 { get; set; }
        public string FileLocation3 { get; set; }

        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public bool IsEnabled { get; set; }
        public string ProductGroupNM { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public int CushionTypeID { get; set; }
        public string CushionTypeNM { get; set; }
        public int DisplayIndex { get; set; }

        public int UpdatedBy { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string CreatorName { get; set; }

        public List<CushionColorTestReport> CushionColorTestReports { get; set; }
    }
}