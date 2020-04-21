using System.Collections.Generic;

namespace Module.ComplianceMng.DTO
{
    public class SearchFormData
    {
        public List<ComplianceSearchDTO> Data { get; set; }
        public List<ClientDTO> ClientDTOs { get; set; }
        public List<FactoryDTO> FactoryDTOs { get; set; }
        public List<ComplianceCertificateTypeDTO> ComplianceCertificateTypeDTOs { get; set; }
        public List<AuditStatusDTO> AuditStatusDTOs { get; set; }        
    }
}
