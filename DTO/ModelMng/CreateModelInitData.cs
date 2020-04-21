using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class CreateModelInitData
    {
        public List<Support.ProductType> ProductTypes { get; set; }

        public List<Support.PackagingMethod> PackagingMethods { get; set; }
        public List<ModelCheckListGroupDTO> ModelCheckListGroupDTOs { get; set; }
        public List<Support.Season> Seasons { get; set; }

        public List<Support.ProductGroup> ProductGroups { get; set; }

        public List<Support.ManufacturerCountry> ManufacturerCountries { get; set; }

        public List<Support.ModelStatus> ModelStatuses { get; set; }

        public List<Support.ProductCategory> ProductCategories { get; set; }

        public List<Support.Factory> Factories { get; set; }

        public List<Support.GalleryItemType> GalleryItemTypes { get; set; }

        public List<Support.ProductElement> ProductElements { get; set; }

        public List<ProductElementOption> ProductElementOptions { get; set; }

        public List<Support.MaterialType> MaterialTypes { get; set; }

        public List<QCTrackingStatus> SupportQCTrackingStatus { get; set; }

        public List<QCTrackingType> SupportQCTrackingType { get; set; }
    }
}
