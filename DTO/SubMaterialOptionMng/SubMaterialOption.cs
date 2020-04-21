using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SubMaterialOptionMng
{
    public class SubMaterialOption
    {
        public int SubMaterialOptionID { get; set; }
        public string SubMaterialOptionUD { get; set; }
        public string SubMaterialOptionNM { get; set; }
        public int? SubMaterialID { get; set; }
        public int? SubMaterialColorID { get; set; }
        public string ImageFile { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }

        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string UpdatorName { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }

        public bool IsEnabled { get; set; }
        public int DisplayIndex { get; set; }
        public string FullName { get; set; }
        public string CreatedDate { get; set; }

        public int? CreatedBy { get; set; }


        public string CreatorFullName { get; set; }
        public string CreatorName { get; set; }
    }
}