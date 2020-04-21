using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientSpecificationMng.DTO
{
    public class EditFormDTO
    {
        public ClientSpecificationDTO ResultData { get; set; }
        public List<ClientSpecificationTypeDTO> ClientSpecificationTypes { get; set; }
    }
}
