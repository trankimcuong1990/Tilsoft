using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BizBloqSyncMng.DTO
{
    public class StockDTO
    {
        public string articleCode { get; set; }
        public string subEANCode { get; set; }
        public int availableQnt { get; set; }
        public int stockQnt { get; set; }
    }
}
