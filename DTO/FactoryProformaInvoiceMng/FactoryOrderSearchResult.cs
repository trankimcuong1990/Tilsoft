using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryProformaInvoiceMng
{
    public class FactoryOrderSearchResult
    {
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ItemName { get; set; }
        public string LDS { get; set; }
        public string Season { get; set; }
        public int? FactoryID { get; set; }
    }
}
