using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MaterialOptionMng
{
    public class MaterialOption
    {
        public int MaterialOptionID { get; set; }
        public string MaterialOptionUD { get; set; }
        public string MaterialOptionNM { get; set; }
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public int? MaterialColorID { get; set; }
        public string ImageFile { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public int DisplayIndex { get; set; }
        public string Remark { get; set; }

        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string UpdatorName { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }

        public bool IsEnabled { get; set; }
        public List<DTO.MaterialOptionMng.ProductGroup> ProductGroups { get; set; }

        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public string CreatorName { get; set; }
        public List<DTO.MaterialOptionMng.MaterialOptionTestReport> MaterialOptionTestReports { get; set; }
        public List<DTO.MaterialOptionMng.MaterialTestingDTO> MaterialTestingDTOs { get; set; }
    }
}