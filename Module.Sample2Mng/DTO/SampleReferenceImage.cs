using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleReferenceImage
    {
        public int SampleReferenceImageID { get; set; }
        public string FileUD { get; set; }
        public string BringInBy { get; set; }
        public string BringInDate { get; set; }
        public string Remark { get; set; }
        public bool? IsDefault { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }

        public bool HasChanged { get; set; }
        public string NewFile { get; set; }
    }
}
