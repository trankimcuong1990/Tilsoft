using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientCityMng
{
    public class ClientCity
    {
        [Required]
        public int ClientCityID { get; set; }


        [Display(Name = "Client City Code")]
        public string ClientCityUD { get; set; }

        [Required]
        [Display(Name = "Client City Name")]
        public string ClientCityNM { get; set; }



        [Display(Name = "Updator Name")]
        public string UpdatorName { get; set; }


        [Display(Name = "Date Updated")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [Display(Name = "Creator Name")]
        public string CreatorName { get; set; }


        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> CreatedDate { get; set; }


        [Display(Name = "Concurrency Flag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "ClientCountryNM")]
        public string ClientCountryNM { get; set; }

        public int? ClientCountryID { get; set; }
        public List<DTO.ClientCountryMng.ClientCountry> ClientCountrys { get; set; }

        [Display(Name = "CreatedBy")]
        public int CreatedBy { get; set; }

        public string CreatedDateFormated { get; set; }

        [Display(Name = "UpdatedBy")]
        public int UpdatedBy { get; set; }

        public string UpdatedDateFormated { get; set; }
    }
}
