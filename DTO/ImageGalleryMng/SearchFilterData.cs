using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ImageGalleryMng
{
    public class SearchFilterData
    {
        public List<Support.GalleryItemType> GalleryItemTypes { get; set; }
        public List<Support.SeasonType> SeasonTypes { get; set; }
        public List<Support.YesNo> YesNos { get; set; }
    }
}
