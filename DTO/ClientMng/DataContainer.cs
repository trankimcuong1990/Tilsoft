using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class DataContainer
    {
        public Client Client { get; set; }
        public List<Support.Saler> Salers { get; set; }
        public List<AccountManagerDTO> AccountManagerDTOs { get; set; }
        public List<Support.PaymentTerm> PaymentTerms { get; set; }
        public List<Support.DeliveryTerm> DeliveryTerms { get; set; }
        public List<Support.Agents> Agents { get; set; }
        public List<Support.ClientCity> ClientCities { get; set; }
        public List<Support.ClientCountry> ClientCountries { get; set; }
        public List<Support.ClientGroup> ClientGroups { get; set; }
        public List<Support.ClientRelationshipType> ClientRelationshipTypes { get; set; }
        public List<Support.ClientType> ClientTypes { get; set; }
        public List<Support.Language> Languages { get; set; }
        public List<Support.Season> Seasons { get; set; }
        public List<Support.ClientRating> ClientRatings { get; set; }
        public List<Support.ReportTemplate> ReportTemplates { get; set; }
        public List<DTO.ClientMng.ClientDocumentTypeDTO> ClientDocumentTypeDTOs { get; set; }
        public List<DTO.ClientMng.ClientAdditionalConditionDTO> ClientAdditionalConditionDTO { get; set; }
        public List<DTO.ClientMng.ClientGetDataAdditionalConditionDTO> ClientGetDataAdditionalConditionDTO { get; set; }
        public List<DTO.ClientMng.ClientBusinessCardDTO> ClientBusinessCardDTO { get; set; }
        public List<DTO.ClientMng.ClientAdditionalConditionTypeDTO> ClientAdditionalConditionTypeDTO { get; set; }
        public List<DTO.ClientMng.UsersDTO> UsersDTO { get; set; }
    }
}
