using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.AccPIOverviewRpt
{
    public class HtmlOverview
    {
        public object PrimaryID { get; set; }
        public string Season { get; set; }
        public string Currency { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SaleNM { get; set; }
        public string ConfirmedStatus { get; set; }
        public string LDS { get; set; }
        public string ETA { get; set; }
        public string DeliveryDate { get; set; }
        public string PIReceivedDate { get; set; }
        public string Suppliers { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? EURAmount { get; set; }
        public decimal? TotalEURAmount { get; set; }
        public string PaymentTermNM { get; set; }
        public int? IsDPReceived { get; set; }
        public decimal? TotalPDAmount { get; set; }
        public int? IsLCReceived { get; set; }
        public string ReceivedDate { get; set; }
        public string FactoryOrderUD { get; set; }
    }
}