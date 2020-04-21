using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class SaleOrderDTO
    {
        public int? SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int? ClientID { get; set; }
        public int? Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
    }
}
