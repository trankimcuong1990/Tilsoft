using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class ClientOfferAdditionalDetailDTO
    {
        public int? ClientOfferAdditionalDetailID { get; set; }
        public int? ClientOfferID { get; set; }
        public int? ClientAdditionalID { get; set; }
        public decimal? Additional20DC { get; set; }
        public decimal? Additional40DC { get; set; }
        public decimal? Additional40HC { get; set; }
        public int? ClientAdditionalItemID { get; set; }
        public string ClientAdditionalItemNM { get; set; }
    }
}
