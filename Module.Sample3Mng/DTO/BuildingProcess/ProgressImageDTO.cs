using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.BuildingProcess
{
    public class ProgressImageDTO
    {
        public int SampleProgressImageID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string NewFile { get; set; }
        public bool HasChanged { get; set; }
    }
}
