using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DefectsMng.DTO
{
    public class DefectsImageDTO
    {
        public int DefectImageID { get; set; }
        public Nullable<int> DefectID { get; set; }
        public string FileUD { get; set; }
        public string Remark { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }

        public string FriendlyName { get; set; }
        public string ScanFileLocation { get; set; }
        public bool ScanHasChange { get; set; }
        public string ScanNewFile { get; set; }
    }
}
