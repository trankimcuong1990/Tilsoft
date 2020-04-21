using System.Collections.Generic;

namespace Module.TransportCostForwarder.DTO
{
    public class ForwarderList
    {
        public List<Support.DTO.Forwarder> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
