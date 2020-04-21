using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShippingInstructionMng
{
    public class SaleOrderSearchResult
    {
        public bool IsSelected { get; set; }
        public int SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int ClientID { get; set; }
    }
}
