using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class ProductSetEANCode
    {
        public int ProductSetEANCodeID { get; set; }
        public int? ProductID { get; set; }
        public string EANCode { get; set; }
        public int? EANCodeIndex { get; set; }
        public List<ProductColli> ProductCollis { get; set; }
    }
}
