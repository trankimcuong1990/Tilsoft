using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class SearchData
    {
        public List<PurchaseOrderSearchDto> Data { get; set; }
        public List<Support.DTO.ConstantEntry> PurchaseOrderStatus { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
    }
}
