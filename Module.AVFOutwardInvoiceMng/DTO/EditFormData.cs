using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFOutwardInvoiceMng.DTO
{
    public class EditFormData
    {
        public Module.AVFOutwardInvoiceMng.DTO.AVFOutwardInvoice Data { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
    }
}
