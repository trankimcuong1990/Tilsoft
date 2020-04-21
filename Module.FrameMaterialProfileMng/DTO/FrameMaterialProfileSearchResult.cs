using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FrameMaterialProfileMng.DTO
{
    public class FrameMaterialProfileSearchResult
    {
        public int FrameMaterialProfileID { get; set; }
        public string FrameMaterialProfileUD { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string Thickness { get; set; }
        public int? FrameMaterialID { get; set; }
        public string FrameMaterialNM { get; set; }
        public int? FactoryID { get; set; }
        public string Factory { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        public string UpdatorName { get; set; }
    }
}