using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.QARemark
{
    public class RemarkImageDTO
    {
        public int SampleQARemarkImageID { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string NewFile { get; set; }
        public bool HasChanged { get; set; }
    }
}
