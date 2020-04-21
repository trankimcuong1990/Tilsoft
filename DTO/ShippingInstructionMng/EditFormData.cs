using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShippingInstructionMng
{
    public class EditFormData
    {
        public ShippingInstruction Data { get; set; }
        public List<DTO.Support.Client> Clients { get; set; }
        public List<DTO.Support.POD> PODs { get; set; }
        public List<DTO.Support.ClientCountry> Countries { get; set; }
        public List<DTO.Support.Forwarder> Forwarders { get; set; }
        public List<DTO.Support.NotifyType> NotifyTypes { get; set; }
        public List<DTO.Support.ConsigneeType> ConsigneeTypes { get; set; }
    }
}
