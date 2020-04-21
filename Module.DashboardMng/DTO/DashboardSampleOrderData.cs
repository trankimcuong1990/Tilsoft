using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardSampleOrderData
    {
        public int SampleOrderID { get; set; }
        public string SampleOrderUD { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string Deadline { get; set; }
        public string DisplayText { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public bool? IsURLLink { get; set; }

        public List<DashboardSampleProductData> SampleProductData { get; set; }
    }
}
