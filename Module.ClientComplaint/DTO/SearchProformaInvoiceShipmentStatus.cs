using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientComplaint.DTO
{
    public class SearchProformaInvoiceShipmentStatus
    {
        public int? ClientID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int? FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public int FactoryOrderDetailID { get; set; }
        public bool? IsShipped { get; set; }
        public bool? IsLoaded { get; set; }
        public string LDS { get; set; }
    }
}
