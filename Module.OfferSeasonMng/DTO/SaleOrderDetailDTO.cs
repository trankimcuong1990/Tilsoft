using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class SaleOrderDetailDTO
    {
        public int SaleOrderDetailID { get; set; }
        public Nullable<int> OfferLineID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ProformaInvoiceNo { get; set; }
    }
}
