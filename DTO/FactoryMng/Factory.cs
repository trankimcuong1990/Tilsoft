using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryMng
{
    public class Factory
    {
        public int FactoryID { get; set; }

        [Display(Name = "FactoryUD")]
        public string FactoryUD { get; set; }

        [Display(Name = "FactoryName")]
        public string FactoryName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "TaxCode")]
        public string TaxCode { get; set; }

        [Display(Name = "PIC")]
        public string PIC { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Description")]
        public object Description { get; set; }

        [Display(Name = "SupplierID")]
        public int? SupplierID { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "LocationID")]
        public int? LocationID { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        public string ConcurrencyFlag_String { get; set; }
    }
}