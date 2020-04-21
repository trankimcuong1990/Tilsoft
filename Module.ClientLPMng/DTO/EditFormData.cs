using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientLPMng.DTO
{
    public class EditFormData
    {
        public DTO.ClientLPDTO Data { get; set; }
        public List<SupportEmployeeDTO> SupportEmployeeDTOs { get; set; }
        public List<SupportLPManagingDTO> SupportLPManagingDTOs { get; set; }
    }
}
