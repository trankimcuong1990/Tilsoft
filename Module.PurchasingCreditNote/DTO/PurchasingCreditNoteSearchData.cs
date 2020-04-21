namespace Module.PurchasingCreditNote.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PurchasingCreditNoteSearchData
    {
        public List<PurchasingCreditNote> Data { get; set; }
        public List<Support.DTO.Factory> DataFactory { get; set; }
    }
}
