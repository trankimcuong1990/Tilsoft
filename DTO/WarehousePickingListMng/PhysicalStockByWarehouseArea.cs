using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehousePickingListMng
{
    public class PhysicalStockByWarehouseArea
    {
        public object KeyID { get; set; }
        public int GoodsID { get; set; }
        public int ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public int WarehouseAreaID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProductStatusNM { get; set; }
        public string WarehouseAreaUD { get; set; }
        public int? PhysicalQnt { get; set; }

    }
}
