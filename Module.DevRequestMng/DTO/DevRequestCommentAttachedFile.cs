using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestMng.DTO
{
    public class DevRequestCommentAttachedFile
    {
        public int DevRequestCommentAttachedFileID { get; set; }
        public string Description { get; set; }
        public string FileUD { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }

        public string NewFile { get; set; }
        public bool HasChanged { get; set; }
    }
}
