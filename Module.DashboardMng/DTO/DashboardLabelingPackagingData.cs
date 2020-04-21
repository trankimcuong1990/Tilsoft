using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardLabelingPackagingData
    {
        public int LabelingPackagingID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string LPStatusNM { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasURLLink { get; set; }
        public int? LPStatusID { get; set; }
    }
}
