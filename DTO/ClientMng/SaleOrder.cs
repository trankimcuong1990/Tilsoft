using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class SaleOrder
    {
        public int? SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public int? ClientID { get; set; }
    }
}
