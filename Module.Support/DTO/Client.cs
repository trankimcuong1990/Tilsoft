using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class Client
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string ZipCode { get; set; }
        public int? PaymentTermID { get; set; }
        public int? DeliveryTermID { get; set; }
        public int? SaleID { get; set; }
        public string ClientContactName { get; set; }
        public string ClientCityNM { get; set; }
        public string ClientCountryNM { get; set; }
        public string FullAddress { get; set; }

        public int? Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
    }
}
