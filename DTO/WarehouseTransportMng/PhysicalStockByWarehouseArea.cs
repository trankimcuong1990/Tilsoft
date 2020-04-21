using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseTransportMng
{
    public class PhysicalStockByWarehouseArea
    {
        public object KeyID { get; set; }
        public int? GoodsID { get; set; }
        public int? ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public int? WarehouseAreaID { get; set; }
        public string WarehouseAreaUD { get; set; }
        public int? PhysicalQnt { get; set; }
    }
}
