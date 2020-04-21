using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ModuleStatusMng.DTO
{
    public class SupportFormData
    {
        public SupportFormData()
        {
            Modules = new List<ModuleDTO>();
        }

        public List<ModuleDTO> Modules { get; set; }
    }
}
