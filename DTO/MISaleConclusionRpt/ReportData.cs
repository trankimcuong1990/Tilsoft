using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleConclusionRpt
{
    public class ReportData
    {
       
        //4 FIRST LIST
        public List<ProformaCountry> ProformaCountries { get; set; }
        public List<ConfirmedProformaCountry> ConfirmedProformaCountries { get; set; }
        public List<ExpectedCountry> ExpectedCountries { get; set; }
        public List<CommercialInvoiceCountry> CommercialInvoiceCountries { get; set; }

        //EXPECTED: TOP 10 CLIENTS BY COUNTRY, CLIENT & SALES (EXPECTED - 2017/2018), DISTRIBUTION OF CLIENTS (EXPECTED - 2017/2018) 
        public List<ExpectedTopClient> ExpectedTopClients { get; set; }
        public List<RangeExpected> RangeExpected { get; set; }
        public List<Expected> Expected { get; set; }

        //PROFORMA INVOICE CONFIRMED: TOP 10 CLIENTS BY COUNTRY, CLIENT & SALES(PI CONFIRMED - 2017 / 2018), DISTRIBUTION OF CLIENTS(PI CONFIRMED - 2017 / 2018)
        public List<ConfirmedProformaTopClient> ConfirmedProformaTopClients { get; set; }
        public List<RangeConfirmedProforma> RangeConfirmedProformas { get; set; }
        public List<ConfirmedProforma> ConfirmedProformas { get; set; }

        //PROFORMA INVOICE: TOP 10 CLIENTS BY COUNTRY, CLIENT & SALES(PI - 2017 / 2018), DISTRIBUTION OF CLIENTS(PI - 2017 / 2018)
        public List<ProformaInvoiceTopClient> ProformaInvoiceTopClients { get; set; }
        public List<RangeProformaInvoice> RangeProformaInvoices { get; set; }
        public List<ProformaInvoice> ProformaInvoices { get; set; }

        //DELTA
        public List<Delta> Deltas { get; set; }

        //param value
        public decimal ExchangeRate { get; set; }
        public decimal USDContainerValue { get; set; }
        public decimal EURContainerValue { get; set; }
        public List<Client> Clients { get; set; }

    }
}
