using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarServiceMng.DTO
{
    public class WarehousePhysicalStock
    {
        public string KeyID { get; set; }
        public string ProductStatusNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? PhysicalQnt { get; set; }
    }
}
