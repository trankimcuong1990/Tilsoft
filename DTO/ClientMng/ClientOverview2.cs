using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientOverview2
    {
        public List<DTO.ClientMng.ClientOffer> Offers { get; set; }
        public List<DTO.ClientMng.ClientPLC> PLCs { get; set; }
        public List<DTO.ClientMng.PLCSaleOrder> PLCSaleOrders { get; set; }
        public List<DTO.ClientMng.PLCLoadingPLan> PLCLoadingPLans { get; set; }
        public List<DTO.ClientMng.ClientECommercialInvoice> CommercialInvoices { get; set; }
        public List<DTO.ClientMng.ClientSampleOrder> SampleOrders { get; set; }
        public List<DTO.ClientMng.ClientComplaint> ClientComplaints { get; set; }
        public List<DTO.ClientMng.ClientSaleOrder> SaleOrders { get; set; }
    }
}
