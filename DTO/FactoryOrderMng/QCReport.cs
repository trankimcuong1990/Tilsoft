using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class QCReport
    {
        public int QCReportID { get; set; }
        public string QCReportUD { get; set; }
        public string QCReportNM { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FriendlyName { get; set; }
        public string Remark { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
