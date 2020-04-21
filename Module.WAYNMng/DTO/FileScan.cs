using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WAYNMng.DTO
{
    public class FileScan
    {
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        //
        // PDF
        //
        public bool FileScan_HasChange { get; set; }
        public string FileScan_NewFile { get; set; }
        public string WorkingDate { get; set; }
    }
}
