using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FrameMaterialOptionMng
{
    public class FrameMaterialOptionMaterialOption
    {
        public int FrameMaterialOptionMaterialOptionID { get; set; }
        public int MaterialOptionID { get; set; }
        public string MaterialOptionUD { get; set; }
        public string MaterialOptionNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
    }
}