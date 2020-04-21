using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DTO.ProductMng
{
    public class ProductSearchResult
    {
        public int ProductID { get; set; }
        public bool IsConfirmed { get; set; }
        public string Season { get; set; }
        public int CataloguePageNumber { get; set; }
        public string ProductTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FrameMaterialNM { get; set; }
        public string MaterialOptionNM { get; set; }
        public string CushionOptionNM { get; set; }
        public string UpdatorName1 { get; set; }
        public string UpdatorName2 { get; set; }
        public string UpdatedDate { get; set; }
        public string ConfirmerName1 { get; set; }
        public string ConfirmerName2 { get; set; }
        public string ConfirmedDate { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        public string EANCode { get; set; }
        public int? ConfirmedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}