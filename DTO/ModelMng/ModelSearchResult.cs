using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelMng
{
    public class ModelSearchResult
    {
        public int ModelID { get; set; }

        [Display(Name = "ModelUD")]
        public string ModelUD { get; set; }

        [Display(Name = "ModelNM")]
        public string ModelNM { get; set; }

        [Display(Name = "EANCode")]
        public string EANCode { get; set; }

        [Display(Name = "RangeName")]
        public string RangeName { get; set; }

        [Display(Name = "Collection")]
        public string Collection { get; set; }

        [Display(Name = "ProductTypeNM")]
        public string ProductTypeNM { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }
        public bool IsStandard { get; set; }
        public int? CataloguePageNo { get; set; }
        public int? ManufacturerCountryID { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string ModelStatusNM { get; set; }
        public int? ProductCategoryID { get; set; }
        public string ProductCategoryNM { get; set; }
        public string FactoryUD { get; set; }

        public bool HasCushion { get; set; }
        public bool HasFrameMaterial { get; set; }
        public bool HasSubMaterial { get; set; }
        public string Season { get; set; }
        public string ProductGroupNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        public bool IsExcludedInNotification { get; set; }
        public bool IsArchived { get; set; }

        public int UpdatedBy { get; set; }
        public string UpdatorName2 { get; set; }

        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public string CreatorName { get; set; }
        public string CreatorName2 { get; set; }


        public bool IsDefaultConfirmed { get; set; }
    }
}