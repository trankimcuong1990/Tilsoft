using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ForwarderMng
{
    public class EditFormData
    {
        public DTO.ForwarderMng.Forwarder Data { get; set; }
        public List<DTO.ForwarderMng.SupportEmployee> Employees { get; set; }
    }
}
