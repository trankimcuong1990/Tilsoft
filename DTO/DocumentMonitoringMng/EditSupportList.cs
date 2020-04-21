using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DocumentMonitoringMng
{
    public class EditSupportList
    {
        public List<DefaultRemark> DefaultRemarks { get; set; }
        public List<DTO.Support.User> Users { get; set; }
    }
}
