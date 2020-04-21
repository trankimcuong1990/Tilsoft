using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class BillTransport
    {
        public int? BookingID { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }
        public int? ClientID { get; set; }
        public string PoLName { get; set; }
        public int? POLID { get; set; }
        public int? Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public List<ContainerTransport> ContainerTransports { get; set; }
        public List<ClientOfferCostItem> ClientOfferCostItems { get; set; }
    }
}
