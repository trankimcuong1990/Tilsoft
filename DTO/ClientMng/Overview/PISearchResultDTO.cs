using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview
{
    public class PISearchResultDTO
    {
        public int OfferID { get; set; }
        public int SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
    }
}
