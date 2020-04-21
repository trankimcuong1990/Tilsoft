using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SampleMng
{
    public class SampleReferenceImage
    {
        public int SampleReferenceImageID { get; set; }

        public int? SampleProductID { get; set; }

        public string FileUD { get; set; }

        public string BringInBy { get; set; }

        public string BringInDate { get; set; }

        public string Remark { get; set; }
        public bool? IsDefault { get; set; }

        public string ThumbnailLocation { get; set; }

        public string FileLocation { get; set; }
        public bool HasChange { get; set; }
        public string NewFile { get; set; }

        public string ConcurrencyFlag { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
    }
}