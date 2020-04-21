using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportOffer.DTO
{
    public class TransportConditionItemDTO
    {
        public int? TransportConditionItemID { get; set; }
        public string TransportConditionItemNM { get; set; }
        public bool? IsDefault { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
    }
}
