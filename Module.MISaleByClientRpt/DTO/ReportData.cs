using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByClientRpt.DTO
{
    public class ReportData
    {
        public decimal ExchangeRate { get; set; }
        public decimal USDContainerValue { get; set; }
        public decimal EURContainerValue { get; set; }
        
        public List<EurofarCommercialInvoiceOfNewClient> EurofarCommercialInvoiceOfNewClientThisSeason { get; set; }
        public List<EurofarCommercialInvoiceOfLostedClient> EurofarCommercialInvoiceOfLostedClientThisSeason { get; set; }
        public List<EurofarCommercialInvoiceOfNewClient> EurofarCommercialInvoiceOfNewClientLastSeason { get; set; }
        public List<EurofarCommercialInvoiceOfLostedClient> EurofarCommercialInvoiceOfLostedClientLastSeason { get; set; }
        public List<ProformaInvoiceOfLostedClient> ProformaInvoiceOfLostedClients { get; set; }
        public List<ProformaInvoiceOfNewClient> ProformaInvoiceOfNewClients { get; set; }

        public ReportData()
        {
            ProformaInvoiceOfLostedClients = new List<ProformaInvoiceOfLostedClient>();
            ProformaInvoiceOfNewClients = new List<ProformaInvoiceOfNewClient>();
            EurofarCommercialInvoiceOfNewClientThisSeason = new List<EurofarCommercialInvoiceOfNewClient>();
            EurofarCommercialInvoiceOfLostedClientThisSeason = new List<EurofarCommercialInvoiceOfLostedClient>();
            EurofarCommercialInvoiceOfNewClientLastSeason = new List<EurofarCommercialInvoiceOfNewClient>();
            EurofarCommercialInvoiceOfLostedClientLastSeason = new List<EurofarCommercialInvoiceOfLostedClient>();
            ExchangeRate = 0;
            USDContainerValue = 0;
            EURContainerValue = 0;
        }       
    }
}

