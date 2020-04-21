using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MaterialOptionMng
{
    public class MaterialTestingFileDTO
    {
        public int MaterialTestReportFileID { get; set; }
        public int? MaterialTestReportID { get; set; }
        public string Remark { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
    }
}
