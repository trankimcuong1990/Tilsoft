using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class ClientAdditionalItemDTO
    {
        public int? ClientAdditionalItemID { get; set; }
        public string ClientAdditionalItemNM { get; set; }
        public bool? IsDefault { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
    }
}
