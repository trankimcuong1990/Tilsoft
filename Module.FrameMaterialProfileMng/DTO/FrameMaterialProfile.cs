using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FrameMaterialProfileMng.DTO
{
    public class FrameMaterialProfile
    {
        public int? FrameMaterialProfileID { get; set; }
        public string FrameMaterialProfileUD { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string Thickness { get; set; }
        public int? FrameMaterialID { get; set; }
        public string FrameMaterialNM { get; set; }
        public string UpdatorName { get; set; }
        public string FactoryUD { get; set; }

        public List<FrameMaterialProfileFactory> FrameMaterialProfileFactories { get; set; }
        //
        // Picture
        //
        public string ImageFile { get; set; }
        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string ImageFile_DisplayUrl { get; set; }

        //
        // concurrency
        //
        public string ConcurrencyFlag_String { get; set; }
    }
}
