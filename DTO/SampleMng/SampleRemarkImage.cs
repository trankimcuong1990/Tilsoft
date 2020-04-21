using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SampleMng
{
    public class SampleRemarkImage
    {
        public int SampleRemarkImageID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string OriginalFileLocation { get; set; }
        public string ThumbnailFileLocation { get; set; }
        public bool HasChange { get; set; }
        public string NewFile { get; set; }
    }
}