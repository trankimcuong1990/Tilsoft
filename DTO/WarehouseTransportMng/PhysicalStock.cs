using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseTransportMng
{
    public class PhysicalStock
    {
        public object KeyID { get; set; }
        public int? GoodsID { get; set; }
        public int? ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProductStatusNM { get; set; }
        public int? PhysicalQnt { get; set; }
    }
}
