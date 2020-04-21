using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryCreditNoteMng.DTO
{
    public class FactoryCreditNoteSearchResult
    {
        public int FactoryCreditNoteID { get; set; }
        public bool? IsConfirmed { get; set; }
        public string Season { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }
        public string CreditNoteNo { get; set; }
        public string IssuedDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Remark { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public int? SupplierID { get; set; }
    }
}
