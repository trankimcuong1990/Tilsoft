using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryMng2.DTO
{
    public class FactoryImage
    {
        public int FactoryImageID { get; set; }
        public int? FactoryID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public bool HasChange { get; set; }
        public string NewFile { get; set; }
        public int? ImageIndex { get; set; }
    }
}
