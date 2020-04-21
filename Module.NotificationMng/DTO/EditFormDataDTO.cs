using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng.DTO
{
    public class EditFormDataDTO
    {
        public DTO.NotificationGroupDTO Data { get; set; }
        public List<ModuleDTO> Modules { get; set; }

        public EditFormDataDTO()
        {
            Modules = new List<ModuleDTO>();
        }
    }
}
