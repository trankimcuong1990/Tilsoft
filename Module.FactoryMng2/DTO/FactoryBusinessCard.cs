using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryBusinessCard
    {
        public int FactoryBusinessCardID { get; set; }
        public int? FactoryID { get; set; }
        public string FrontFileUD { get; set; }
        public string BehindFileUD { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public string FrontFileLocation { get; set; }
        public string FrontThumbnailLocation { get; set; }
        public string BehindFileLocation { get; set; }
        public string BehindThumbnailLocation { get; set; }

        public string FrontFriendlyName { get; set; }
        public bool FrontHasChange { get; set; }
        public string FrontNewFile { get; set; }

        public string BehindFriendlyName { get; set; }
        public bool BehindHasChange { get; set; }
        public string BehindNewFile { get; set; }
        public int? ControllerID { get; set; }
        public string ControllerName { get; set; }
        public string OtherName { get; set; }
    }
}
