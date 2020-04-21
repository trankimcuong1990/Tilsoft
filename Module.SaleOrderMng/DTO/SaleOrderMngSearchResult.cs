using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class SaleOrderMngSearchResult
    {
        public int SaleOrderID { get; set; }
        public int? OfferID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorNM { get; set; }
        public string UpdatedDate { get; set; }
    }
}
