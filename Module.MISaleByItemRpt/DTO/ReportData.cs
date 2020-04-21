using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByItemRpt.DTO
{
    public class ReportData
    {
        public decimal ExchangeRate { get; set; }
        public decimal USDContainerValue { get; set; }
        public decimal EURContainerValue { get; set; }
        public List<DTO.Top20ByCategory> Top20ByCategories { get; set; }
        public List<DTO.Top30> Top30s { get; set; }
        public List<DTO.ItemByClient> ItemByClients { get; set; }
        public List<DTO.CommercialInvoiceByCategories> CommercialInvoiceByCategories { get; set; }
        public ReportData()
        {
            Top20ByCategories = new List<Top20ByCategory>();
            Top30s = new List<Top30>();
            ItemByClients = new List<ItemByClient>();
            CommercialInvoiceByCategories = new List<CommercialInvoiceByCategories>();
            ExchangeRate = 0;
            USDContainerValue = 0;
            EURContainerValue = 0;
        }
    }
}
