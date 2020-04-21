using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseImportMng
{
    public class ProductColli
    {
        public int ProductColliID { get; set; }
        public string ColliEANCode { get; set; }
        public int? ProductID { get; set; }
        public string ColliDescription { get; set; }
    }
}
