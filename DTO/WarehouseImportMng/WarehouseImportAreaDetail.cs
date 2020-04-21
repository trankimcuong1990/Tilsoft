using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseImportMng
{
    public class WarehouseImportAreaDetail
    {
        public int WarehouseImportAreaDetailID { get; set; }
        public int? WarehouseImportProductDetailID { get; set; }
        public int? WarehouseImportSparepartDetailID { get; set; }
        public int? WarehouseAreaID { get; set; }
        public int? Quantity { get; set; }
        public string Remark { get; set; }
        public bool? IsShowRemark { get; set; }
    }
}
