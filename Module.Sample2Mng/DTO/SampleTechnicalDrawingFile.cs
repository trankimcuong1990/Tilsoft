using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleTechnicalDrawingFile
    {
        public int SampleTechnicalDrawingFileID { get; set; }
        public bool? IsActived { get; set; }
        public string Remark { get; set; }
        public string PreviewFileUD { get; set; }
        public string SourceFileUD { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string SourceFileFriendlyName { get; set; }
        public string SourceFileLocation { get; set; }
        public string PreviewFileFriendlyName { get; set; }
        public string PreviewFileLocation { get; set; }


        public bool SourceFileHasChanged { get; set; }
        public string SourceFileNewFile { get; set; }

        public bool PreviewFileHasChanged { get; set; }
        public string PreviewFileNewFile { get; set; }
    }
}
