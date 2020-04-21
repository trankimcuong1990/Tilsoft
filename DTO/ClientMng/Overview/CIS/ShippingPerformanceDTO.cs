using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.CIS
{
    public class ShippingPerformanceDTO
    {
        public int SaleOrderID { get; set; }
        public int OfferID { get; set; }
        public int? ClientID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public int LoadingPlanID { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ShippedDate { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public int? OverDue { get; set; }
        public string LoadingDate { get; set; }
    }
}
