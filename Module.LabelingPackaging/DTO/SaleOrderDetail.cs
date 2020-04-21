using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    public class SaleOrderDetail
    {
        public int? SaleOrderDetailID { get; set; }
        public int? SaleOrderID { get; set; }
        public string ClientEANCode { get; set; }
    }
}
