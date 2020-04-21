using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryCreditNoteMng.DTO
{
    public class FactoryCreditNote
    {
        public int FactoryCreditNoteID { get; set; }
        public string Season { get; set; }
        public string CreditNoteNo { get; set; }
        public string IssuedDate { get; set; }
        public int? SupplierID { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }

        public List<FactoryCreditNoteDetail> FactoryCreditNoteDetails { get; set; }
    }
}
