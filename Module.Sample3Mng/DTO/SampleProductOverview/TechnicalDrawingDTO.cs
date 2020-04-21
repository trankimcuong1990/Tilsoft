using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.SampleProductOverview
{
    public class TechnicalDrawingDTO
    {
        public int SampleTechnicalDrawingFileID { get; set; }
        public string Description { get; set; }
        public string SourceFileFriendlyName { get; set; }
        public string SourceFileLocation { get; set; }
        public string PreviewFileFriendlyName { get; set; }
        public string PreviewFileLocation { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
    }
}
