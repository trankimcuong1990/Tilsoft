using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class ClientConditionItemDTO
    {
        public int? ClientConditionItemID { get; set; }
        public string ClientConditionItemNM { get; set; }
        public bool? IsDefault { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ClientCostTypeNM { get; set; }
        public string ClientCostChargeTypeNM { get; set; }
    }
}
