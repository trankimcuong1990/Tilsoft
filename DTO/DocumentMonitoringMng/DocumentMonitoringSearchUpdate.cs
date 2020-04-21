using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DocumentMonitoringMng
{
    public class DocumentMonitoringSearchUpdate
    {
        public int? DocumentMonitoringID { get; set; }

        public string VNReceivedDate { get; set; }

        public string VNReceiverName { get; set; }

        public string SendToEUDate { get; set; }

        public string SenderToEUName { get; set; }

        public string Remark { get; set; }

        public string Reference { get; set; }

        public int IsEdit { get; set; }
    }
}
