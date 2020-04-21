using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FrameMaterialOptionMng
{
    public class FrameMaterialOptionSearchResult
    {
        public int FrameMaterialOptionID { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string FrameMaterialOptionUD { get; set; }
        public string FrameMaterialOptionNM { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int DisplayIndex { get; set; }
        public string Remark { get; set; }

        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public bool IsEnabled { get; set; }
        public string ProductGroupNM { get; set; }

        public int UpdatedBy { get; set; }
        public string FullName { get; set; }

        public string CreatorName { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }
    }
}