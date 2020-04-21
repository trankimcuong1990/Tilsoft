using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BookingMng
{
    public class EditFormData
    {
        public Booking Data { get; set; }
        public List<DTO.Support.POD> PODs { get; set; }
        public List<DTO.Support.POL> POLs { get; set; }
        public List<DTO.Support.Forwarder> Forwarders { get; set; }
        public List<DTO.Support.DeliveryTerm> DeliveryTerms { get; set; }
        public List<DTO.Support.MovementTerm> MovementTerms { get; set; }
        public List<DTO.Support.StringCollectionItem> OceanFreights { get; set; }
    }
}
