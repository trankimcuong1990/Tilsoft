using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class CreditNoteOverview
    {
        public int? CreditNoteID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public string CreditNoteNo { get; set; }

        public string InvoiceDate { get; set; }

        public string RefNo { get; set; }

        public string Remark { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }
    }
}
