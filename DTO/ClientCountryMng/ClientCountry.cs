using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientCountryMng
{
    public class ClientCountry
    {
        [Required]
        public int ClientCountryID { get; set; }


        [Display(Name = "Client Country Code")]
        public string ClientCountryUD { get; set; }

        [Required]
        [Display(Name = "Client Country Name")]
        public string ClientCountryNM { get; set; }



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

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public string CreatedDateFormated { get; set; }
        public string UpdatedDateFormated { get; set; }
        public string ClientCodeForLog { get; set; }
    }
}
