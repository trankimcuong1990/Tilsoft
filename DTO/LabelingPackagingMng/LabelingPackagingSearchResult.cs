using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LabelingPackagingMng
{
    public class LabelingPackagingSearchResult
    {
        [Display(Name = "SaleOrderID")]
        public int SaleOrderID { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public int Description { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }


        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "PackagingMethodNM")]
        public string PackagingMethodNM { get; set; }

        
    }
}