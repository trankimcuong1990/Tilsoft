using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class EditFormData
    {
        public DTO.Booking Data { get; set; }
        public List<Support.DTO.POL> POLs { get; set; }
        public List<Support.DTO.POD> PODs { get; set; }
        public List<Support.DTO.MovementTerm> MovementTerms { get; set; }
        public List<Support.DTO.DeliveryTerm> DeliveryTerms { get; set; }
        public List<Support.DTO.StringCollectionItem> OceanFreights { get; set; }
        public List<Support.DTO.Forwarder> Forwarders { get; set; }
    }
}
