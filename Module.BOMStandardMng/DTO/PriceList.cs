using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class PriceList
    {
        public int? ProductionItemTypeID { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? CompanyID { get; set; }
        public int? ProductionItemID { get; set; }
        public bool? IsLocked { get; set; }
        public decimal? Price { get; set; }
        public long PrimaryID { get; set; }
    }
}
