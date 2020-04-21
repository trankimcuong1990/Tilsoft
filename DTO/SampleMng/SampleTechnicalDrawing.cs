using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SampleMng
{
    public class SampleTechnicalDrawing
    {
        public int SampleTechnicalDrawingID { get; set; }

        public int SampleProductID { get; set; }

        public string SourceFileUD { get; set; }

        public string PreviewFileUD { get; set; }

        public int? Version { get; set; }

        public int? AuthorID { get; set; }

        public string Remark { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string AuthorName { get; set; }

        public string UpdatorName { get; set; }
        public string ConcurrencyFlag { get; set; }

        public string SourceFileLocation { get; set; }

        public string ThumbnailLocation { get; set; }

        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }

        public bool SourceHasChange { get; set; }
        public string SourceNewFile { get; set; }

        public bool HasChange { get; set; }
        public string NewFile { get; set; }

        public List<SampleRemark> SampleRemarks { get; set; }
    }
}