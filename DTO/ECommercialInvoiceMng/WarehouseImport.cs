using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class WarehouseImport
    {
        public object KeyID { get; set; }

        public int? WarehouseImportID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public string ReceiptNo { get; set; }

        public string ImportedDate { get; set; }

        public string ImportTypeNM { get; set; }

    }
}