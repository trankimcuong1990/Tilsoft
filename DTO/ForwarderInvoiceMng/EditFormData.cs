using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ForwarderInvoiceMng
{
    public class EditFormData
    {
        public DTO.ForwarderInvoiceMng.ForwarderInvoice Data { get; set; }
        public List<DTO.Support.FeeType> FeeTypes { get; set; }
    }
}
