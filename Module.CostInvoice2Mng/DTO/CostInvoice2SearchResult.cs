using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DTO
{
    public class CostInvoice2SearchResult
    {
        public int CostInvoice2ID { get; set; }      
        public decimal? TotalAmount { get; set; }
        public string Season { get; set; }
        public string CostInvoice2CreditorUD { get; set; }
        public int? CostInvoice2TypeID { get; set; }
        public string CostInvoice2TypeNM { get; set; }
        public string RelatedDocumentNo { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public string PaidDate { get; set; }
        public bool? IsPaid { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public string ConfimerNM { get; set; }
        public string UpdatorNM { get; set; }

        public List<CostInvoice2Client> CostInvoice2Clients { get; set; }
        public List<CostInvoice2Factory> CostInvoice2Factories { get; set; }
    }
}
