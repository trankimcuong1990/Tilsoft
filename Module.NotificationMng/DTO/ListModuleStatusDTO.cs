using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng.DTO
{
    public class ListModuleStatusDTO
    {
        public List<ModuleStatusDTO> ModuleStatusDTOs { get; set; }
        public List<NotificationGroupStatusDTO> NotificationGroupStatusDTOs { get; set; }
    }
}
