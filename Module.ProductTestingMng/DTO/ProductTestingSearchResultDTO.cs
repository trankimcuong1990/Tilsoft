using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductTestingMng.DTO
{
    public class ProductTestingSearchResultDTO
    {
        public int ProductTestID { get; set; }
        public string TestDate { get; set; }
        public string ProductTestUD { get; set; }
        public string Remark { get; set; }
        public string UpdatedDate { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ClientUD { get; set; }
        public string TestStandardNM { get; set; }
        public string FullName { get; set; }

        public List<DTO.ProductTestingFileSearchReSultDTO> productTestingFileSearchReSultDTOs { get; set; }
        public List<DTO.ProductTestStandardSearchViewDTO> productTestStandardSearchViewDTOs { get; set; }
    }
}
