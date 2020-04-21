using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialTestingMng.DTO
{
    public class MaterialTestReportSearchDTO
    {
        public int? MaterialTestReportID { get; set; }
        public string MaterialTestReportUD { get; set; }
        public int? MaterialTestStandardID { get; set; }
        public string TestDate { get; set; }
        public string Remark { get; set; }

        public bool? IsEnabled { get; set; }
        public string MaterialOptionUD { get; set; }
        public string MaterialOptionNM { get; set; }

        public string ClientUD { get; set; }
        public string ClientNM { get; set; }

        public string FileUD { get; set; }
	    public string TestStandardNM { get; set; }

        public List<DTO.MaterialFileSearchResultDTO> MaterialTestingFileSearchResultDTOs { get; set; }
        public List<DTO.MaterialTestStandardSearchViewDTO> MaterialTestStandardSearchViewDTOs { get; set; }
    }
}
