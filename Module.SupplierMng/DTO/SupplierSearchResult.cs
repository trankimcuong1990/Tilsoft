using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.SupplierMng.DTO
{
    public class SupplierSearchResult
    {
        public int SupplierID { get; set; }

        [Display(Name = "SupplierUD")]
        public string SupplierUD { get; set; }

        [Display(Name = "SupplierNM")]
        public string SupplierNM { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "PIC")]
        public string PIC { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "ManufacturerCountryNM")]
        public string ManufacturerCountryNM { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int UpdatedBy { get; set; }
        public string CompanyNM { get; set; }
    }
}