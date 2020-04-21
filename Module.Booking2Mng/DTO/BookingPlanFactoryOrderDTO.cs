using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class BookingPlanFactoryOrderDTO
    {
        public long KeyID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> BookingID { get; set; }
        public Nullable<int> PlannedQnt { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> ShippedQnt { get; set; }
    }
}
