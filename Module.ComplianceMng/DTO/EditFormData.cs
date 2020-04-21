using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ComplianceMng.DTO
{
    public class EditFormData
    {
        public ComplianceProcessDTO ComplianceProcessDTO { get; set; }
        public List<ClientDTO> ClientDTOs { get; set; }
        public List<FactoryDTO> FactoryDTOs { get; set; }
        public List<ComplianceCertificateTypeDTO> ComplianceCertificateTypeDTOs { get; set; }
        public List<AuditStatusDTO> AuditStatusDTOs { get; set; }
        public List<EmployeeDTO> EmployeeDTOs { get; set; }
    }
}
