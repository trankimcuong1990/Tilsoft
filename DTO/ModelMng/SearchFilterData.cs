using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class SearchFilterData
    {
        public List<DTO.Support.ProductType> ProductTypes { get; set; }
        public List<DTO.Support.YesNo> StandardValues { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.ProductGroup> ProductGroups { get; set; }
        public List<DTO.Support.ManufacturerCountry> ManufacturerCountries { get; set; }
        public List<DTO.Support.ModelStatus> ModelStatuses { get; set; }
        public List<DTO.Support.ProductCategory> ProductCategories { get; set; }
        public List<DTO.Support.YesNo> YesNoValues { get; set; }

    }
}
