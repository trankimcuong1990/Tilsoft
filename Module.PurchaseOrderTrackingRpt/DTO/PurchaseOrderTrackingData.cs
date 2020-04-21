using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt.DTO
{
    public class PurchaseOrderTrackingData
    {
        public int PurchaseOrderID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string ETA { get; set; }
        public int? PurchaseOrderStatusID { get; set; }

        public List<PurchaseOrderTrackingDetailData> PurchaseOrderTrackingDetail { get; set; }
    }
}
