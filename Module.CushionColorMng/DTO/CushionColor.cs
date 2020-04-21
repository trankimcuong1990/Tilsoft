using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Module.CushionColorMng.DAL;

namespace Module.CushionColorMng.DTO
{
    public class CushionColor
    {
        public int CushionColorID { get; set; }

        [Required]
        public int CushionID { get; set; }

        //[Required]
        public string CushionColorUD { get; set; }

        [Required]
        public string CushionColorNM { get; set; }

        public string ImageFile { get; set; }
        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string ImageFile_DisplayUrl { get; set; }

        // Test 1
        public string TestReportFile1 { get; set; }
        public string FriendlyName1 { get; set; }
        public string FileLocation1 { get; set; }
        public bool TestReportFile_HasChange1 { get; set; }
        public string TestReportFile_NewFile1 { get; set; }

        // Test 2
        public string TestReportFile2 { get; set; }
        public string FriendlyName2 { get; set; }
        public string FileLocation2 { get; set; }
        public bool TestReportFile_HasChange2 { get; set; }
        public string TestReportFile_NewFile2 { get; set; }

        // Test 3
        public string TestReportFile3 { get; set; }
        public string FriendlyName3 { get; set; }
        public string FileLocation3 { get; set; }
        public bool TestReportFile_HasChange3 { get; set; }
        public string TestReportFile_NewFile3 { get; set; }

        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public bool IsEnabled { get; set; }

        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public int? CushionTypeID { get; set; }
        public int DisplayIndex { get; set; }

        public List<CushionColorProductGroup> CushionColorProductGroups { get; set; }

        public int CreatedBy { get; set; }
        public string CreatorName { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }

        public List<CushionColorTestReport> CushionColorTestReports { get; set; }

        public List<DTO.CushionTestingDTO> CushionTestingDTOs { get; set; }
    }
}