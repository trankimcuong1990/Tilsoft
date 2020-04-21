using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class ClientOfferCostDetailDTO
    {
        public int? ClientOfferCostDetailID { get; set; }
        public int? ClientOfferID { get; set; }
        public int? ClientCostItemID { get; set; }
        public int? PODID { get; set; }
        public int? POLID { get; set; }
        public string Currency { get; set; }
        public decimal? Cost20DC { get; set; }
        public decimal? Cost40DC { get; set; }
        public decimal? Cost40HC { get; set; }
        public string Remark { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
