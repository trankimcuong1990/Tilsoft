using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CushionColorMng
{
    public class CushionColorTestReport
    {
        public int CushionColorTestReportID { get; set; }

        public int? CushionColorID { get; set; }

        public string FileUD { get; set; }

        public string FriendlyName { get; set; }

        public string FileLocation { get; set; }

        public string ThumbnailLocation { get; set; }

        public string Remark { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDate { get; set; }

        public bool? File_HasChange { get; set; }

        public string File_NewFile { get; set; }
    }
}
