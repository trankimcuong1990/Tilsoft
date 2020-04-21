using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class ClientDTO
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string PaymentTermNM { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<int> InDays { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<decimal> DownValue { get; set; }
        public string DeliveryTermNM { get; set; }
        public List<ClientEstimatedAdditionalCostDTO> ClientEstimatedAdditionalCostDTOs { get; set; }
    }
}
