using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class ProductColli
    {
        public object RowIndex { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public int? ProductID { get; set; }
        public int? ProductSetEANCodeID { get; set; }
        public int? ProductColliID { get; set; }
        public string ProductEANCode { get; set; }
        public string ColliEANCode { get; set; }
        public bool? IsSelected { get; set; }
    }
}
