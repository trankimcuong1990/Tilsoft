using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class ClientCostItemDTO
    {
        public int? ClientCostItemID { get; set; }
        public string ClientCostItemNM { get; set; }
        public int? ClientCostTypeID { get; set; }
        public string Currency { get; set; }
        public bool? IsDefault { get; set; }
        public int? UpdatedBy { get; set; }
        public int? ClientCostChargeTypeID { get; set; }
        public int? POLID { get; set; }
        public string PoLname { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ClientCostTypeNM { get; set; }
        public string ClientCostChargeTypeNM { get; set; }
    }
}
