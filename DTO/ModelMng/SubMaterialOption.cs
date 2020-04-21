using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelMng
{
    public class SubMaterialOption
    {
        public int ModelSubMaterialOptionID { get; set; }
        public int SubMaterialOptionID { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public int ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string SubMaterialOptionUD { get; set; }
        public string SubMaterialOptionNM { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public bool? IsEnabled { get; set; }
    }
}