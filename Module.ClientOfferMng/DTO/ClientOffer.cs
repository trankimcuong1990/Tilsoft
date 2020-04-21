using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class ClientOffer
    {
        public int? ClientOfferID { get; set; }
        public int? ClientID { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string UpdatorName { get; set; }

        public bool? File_HasChange { get; set; }
        public string File_NewFile { get; set; }

        public List<ClientOfferCostDetailDTO> ClientOfferCostDetailDTOs { get; set; }
        public List<ClientOfferConditionDetailDTO> ClientOfferConditionDetailDTOs { get; set; }
        public List<ClientOfferAdditionalDetailDTO> ClientOfferAdditionalDetailDTOs { get; set; }

        public int? PaymentTermID { get; set; }
        public string PaymentTermNM { get; set; }
    }
}
