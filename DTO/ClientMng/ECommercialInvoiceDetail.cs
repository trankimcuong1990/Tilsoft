using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ECommercialInvoiceDetail
    {
        public object KeyID { get; set; }
        public int? ECommercialInvoiceID { get; set; }
        public int? GoodsID { get; set; }
        public string GoodsType { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public int? TypeOfInvoice { get; set; }
    }
}
