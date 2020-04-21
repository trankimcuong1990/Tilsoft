using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehousePickingListMng
{
    public class WarehousePickingListAreaDetail
    {
        public int WarehousePickingListAreaDetailID { get; set; }
        public int? WarehousePickingListProductDetailID { get; set; }
        public int? WarehousePickingListSparepartDetailID { get; set; }
        public int? WarehouseAreaID { get; set; }
        public int? Quantity { get; set; }
        public string Remark { get; set; }
        public string WarehouseAreaUD { get; set; }
    }
}
