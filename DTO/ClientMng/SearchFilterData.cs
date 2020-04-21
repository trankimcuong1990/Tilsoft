using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class SearchFilterData
    {
        public List<DTO.Support.YesNo> buyingClient { get; set; }
        public List<DTO.Support.Agents> agents { get; set; }
        public List<DTO.Support.DeliveryTerm> deliveryTerms { get; set; }
        public List<DTO.Support.PaymentTerm> PaymentTerms { get; set; }
        public List<Support.ClientRelationshipType> ClientRelationshipTypes { get; set; }
        public List<DTO.ClientMng.AccountManagerDTO> salers { get; set; }
        public List<DTO.Support.ClientCountry> Countries { get; set; }
    }
}
