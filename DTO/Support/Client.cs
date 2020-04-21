using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class Client
    {
        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "StreetAddress1")]
        public string StreetAddress1 { get; set; }

        [Display(Name = "StreetAddress2")]
        public string StreetAddress2 { get; set; }

        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }
        public string ClientAddress { get; set; }
        public int? PaymentTermID { get; set; }
        public int? DeliveryTermID { get; set; }
        public int? SaleID { get; set; }
        public string ClientContactName { get; set; }

        public string ClientCityNM { get; set; }
        public string ClientCountryNM { get; set; }
        public string FullAddress { get; set; }
        public bool IsSelected { get; set; }
        public List<ClientEstimatedAdditionalCostDTO> ClientEstimatedAdditionalCostDTOs { get; set; }

        public Client()
        {
            ClientEstimatedAdditionalCostDTOs = new List<ClientEstimatedAdditionalCostDTO>();
        }
    }

    public class ClientEstimatedAdditionalCostDTO
    {
        public int ClientEstimatedAdditionalCostID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Season { get; set; }
        public Nullable<decimal> InterestCostPercent { get; set; }
        public Nullable<decimal> InspectionCostUSD { get; set; }
        public Nullable<decimal> LCCostPercent { get; set; }
        public Nullable<decimal> SampleCostUSD { get; set; }
    }
}