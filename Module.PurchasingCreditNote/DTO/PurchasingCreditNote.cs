using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingCreditNote.DTO
{
    public class PurchasingCreditNote
    {
        public int? PurchasingCreditNoteID { get; set; }
        public string Season { get; set; }
        public int? CreditNoteType { get; set; }
        public int? PurchasingInvoiceID { get; set; }
        public int? FactoryID { get; set; }
        public string CreditNoteNo { get; set; }
        public string CreditNoteDate { get; set; }
        public string Remark { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }


        public string BLNo { get; set; }
        public string ShipedDate { get; set; }
        public string SupplierNM { get; set; }
        public string ForwarderNM { get; set; }
        public string Feeder { get; set; }
        public string POLName { get; set; }
        public string PODName { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public int? SupplierID { get; set; }
        public string FactoryUD { get; set; }
        public string InvoiceNo { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string InvoiceStatusText { get; set; }
        public string ConfirmerName { get; set; }
        public List<PurchasingCreditNoteDetail> PurchasingCreditNoteDetails { get; set; }
        public List<PurchasingCreditNoteSparepartDetail> PurchasingCreditNoteSparepartDetails { get; set; }
        public List<PurchasingCreditNoteExtendDetail> PurchasingCreditNoteExtendDetails { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }

    }
}
