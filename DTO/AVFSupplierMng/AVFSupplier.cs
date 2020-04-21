using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.AVFSupplierMng
{
    public class AVFSupplier
    {
        public int AVFSupplierID { get; set; }

        [Display(Name = "AVFSupplierUD")]
        public string AVFSupplierUD { get; set; }

        [Display(Name = "AVFSupplierNM")]
        public string AVFSupplierNM { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "PIC")]
        public string PIC { get; set; }

        [Display(Name = "TaxCode")]
        public string TaxCode { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        public decimal? OpeningDebit { get; set; }
        public decimal? OpeningCredit { get; set; }
        public decimal? IncreasingDebit { get; set; }
        public decimal? IncreasingCredit { get; set; }
        public decimal? ClosingDebit { get; set; }
        public decimal? ClosingCredit { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        public string ConcurrencyFlag_String { get; set; }
        public List<AVFSupplierPurchasingInvoice> AVFSupplierPurchasingInvoices { get; set; }

    }
}