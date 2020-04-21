using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CreditNoteMng
{
    public class CreditNote
    {
        public int? CreditNoteID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public string CreditNoteNo { get; set; }

        public string InvoiceDate { get; set; }

        public string RefNo { get; set; }

        public string Remark { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string VATNo { get; set; }

        public string TelePhone { get; set; }

        public string Fax { get; set; }

        public string ClientAddress { get; set; }

        public byte[] ConcurrencyFlag { get; set; }

        public string ConcurrencyFlag_String { get; set; }

        public string AccountNo { get; set; }
        public string Conditions { get; set; }
        public string LCRefNo { get; set; }
        public string Currency { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public string InvoiceNo { get; set; }

        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ConfirmerName { get; set; }


        public List<CreditNoteDetail> CreditNoteDetails { get; set; }
        public List<CreditNoteProductDetail> CreditNoteProductDetails { get; set; }
        public List<CreditNoteSparepartDetail> CreditNoteSparepartDetails { get; set; }

    }
}