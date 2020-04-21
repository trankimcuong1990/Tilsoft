using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class FactoryOrders
    {
        public List<FactoryOrder> Data { get; set; }
        public int TotalRows { get; set; }
    }
    
    public class FactoryOrder
    {
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string Season { get; set; }
    }
}
