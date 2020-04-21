using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LabelingPackagingMng
{
   public class SaleOrder
    {
         public int SaleOrderID {get; set;}
         public string ProformaInvoiceNo { get; set; }

         public DateTime CreatedDate { get; set; }
    }
}
