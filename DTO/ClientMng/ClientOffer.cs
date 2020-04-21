using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientOffer
    {
        public int? OfferID { get; set; }
        public string OfferUD { get; set; }
        public string OfferDate { get; set; }
        public int? OfferVersion { get; set; }
        public string Season { get; set; }
        public string ValidUntil { get; set; }
        public string PaymentTermNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string Currency { get; set; }
        public string LDS { get; set; }
        public string ForwarderNM { get; set; }
        public string PODNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string QntRemark { get; set; }
        public int? ClientID { get; set; }
    }
}
