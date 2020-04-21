using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
   public class WarehouseImportOverView
    {
        public object KeyID { get; set; }

        public int? WarehouseImportID { get; set; }

        public int? SaleOrderID { get; set; }

        public string ReceiptNo { get; set; }

        public string ImportedDate { get; set; }

        public string ImportTypeNM { get; set; }
    }
}
