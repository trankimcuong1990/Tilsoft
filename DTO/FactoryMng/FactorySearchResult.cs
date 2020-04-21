using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryMng
{
    public class FactorySearchResult
    {
        public int FactoryID { get; set; }

        [Display(Name = "FactoryUD")]
        public string FactoryUD { get; set; }

        [Display(Name = "FactoryName")]
        public string FactoryName { get; set; }

        [Display(Name = "SupplierNM")]
        public string SupplierNM { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "LocationNM")]
        public string LocationNM { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "TaxCode")]
        public string TaxCode { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        // AVS) kimcuong 28 JUN 2017 - START
        [Display(Name = "UpdatedBy")]
        public int UpdatedBy { get; set; }
        // AVS) kimcuong 28 JUN 2017 - E N D
    }
}