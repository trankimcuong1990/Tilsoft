using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CreditNoteMng
{
    public class CreditNoteSparepartDetail
    {
        public int? CreditNoteSparepartDetailID { get; set; }

        public int? CreditNoteID { get; set; }

        public int? ECommercialInvoiceSparepartDetailID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public string Remark { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string ContainerNo { get; set; }

        public string SealNo { get; set; }

        public string ContainerTypeNM { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public decimal? Amount { get; set; }

    }
}