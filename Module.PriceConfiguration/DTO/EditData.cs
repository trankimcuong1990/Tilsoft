using DTO.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceConfiguration.DTO
{
    public class EditData
    {
        public PriceConfigurationDto Data { get; set; }

        public List<Module.Support.DTO.Season> SupportSeason { get; set; }
        public List<Module.Support.DTO.ProductElement> SupportProductElement { get; set; }
        public List<Module.Support.DTO.FSCType> SupportFSCType { get; set; }
        public List<Module.Support.DTO.PackagingMethod> SupportPackagingMethod { get; set; }
        public List<Module.Support.DTO.FrameMaterial> SupportFrameMaterial { get; set; }
        public List<MaterialColor> SupportMaterialColor { get; set; }
        public List<DTO.CushionColorDto> SupportCushionColor { get; set; }
        public List<string> SeasonOfPriceConfiguration { get; set; }
    }
}
