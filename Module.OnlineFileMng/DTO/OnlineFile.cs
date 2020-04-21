using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OnlineFileMng.DTO
{
    public class OnlineFile
    {
        public int OnlineFileID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }

        public List<OnlineFilePermission> OnlineFilePermissions { get; set; }

        public string NewFile { get; set; }
        public bool HasChanged { get; set; }
    }
}