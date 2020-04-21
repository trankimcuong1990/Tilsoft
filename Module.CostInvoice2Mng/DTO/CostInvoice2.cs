using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DTO
{
    public class CostInvoice2
    {
        public int CostInvoice2ID { get; set; }
        public string CostInvoice2UD { get; set; }
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public int? CostInvoice2CreditorID { get; set; }
        public string Description { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Currency { get; set; }
        public int? CostInvoice2TypeID { get; set; }
        public string CostInvoice2TypeNM { get; set; }
        public string RelatedDocumentNo { get; set; }
        public string DueDate { get; set; }
        public bool? IsPaid { get; set; }
        public string PaidDate { get; set; }
        public bool? IsChargedToClient { get; set; }
        public bool? IsChargedToFactory { get; set; }
        public string Remark { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public string UpdatorNM { get; set; }
        public string ConfirmerNM { get; set; }

        public string RelatedDocumentFile { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public bool? File_HasChange { get; set; }
        public string File_NewFile { get; set; }

        public List<CostInvoice2Client> CostInvoice2Clients { get; set; }
        public List<CostInvoice2Factory> CostInvoice2Factories { get; set; }
    }
}
