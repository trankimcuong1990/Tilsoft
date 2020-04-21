using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class DraftCommercialInvoiceDetailDTO
    {
        public int DraftCommercialInvoiceDetailID { get; set; }
        public int? DraftCommercialInvoiceID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Remark { get; set; }
        public int? SaleOrderDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int? SaleOrderDetailSparepartID { get; set; }
        public string ArticleCodeSparepart { get; set; }
        public string DescriptionSparepart { get; set; }
        public string ProformaInvoiceNoSparepart { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
        public string ClientArticleCodeSparepart { get; set; }
        public string ClientArticleNameSparepart { get; set; }
        public string ClientOrderNumberSparepart { get; set; }
        public string ClientEANCodeSparepart { get; set; }
        public List<DraftCommercialInvoiceDetailDescriptionDTO> DraftCommercialInvoiceDetailDescriptions {get;set; }
        public DraftCommercialInvoiceDetailDTO()
        {
            DraftCommercialInvoiceDetailDescriptions = new List<DraftCommercialInvoiceDetailDescriptionDTO>();
        }
    }
}
