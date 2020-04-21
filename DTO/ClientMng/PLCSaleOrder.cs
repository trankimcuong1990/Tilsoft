using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class PLCSaleOrder
    {
        public object PrimaryID { get; set; }
        public int? PLCID { get; set; }
        public int? SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
    }
}
