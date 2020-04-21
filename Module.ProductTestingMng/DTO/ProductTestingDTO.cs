using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductTestingMng.DTO
{
    public class ProductTestingDTO
    {
        public int ProductTestID { get; set; }
        public string ProductTestUD { get; set; }
        public int? ProductTestStandardID { get; set; }
        public string TestDate { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? ModelID { get; set; }
        public int? ClientID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string TestStandardNM { get; set; }
        public string UpdatorName { get; set; }

        public List<DTO.ProductTestFileDTO>  productTestFileDTOs { get; set; }
        public List<DTO.ProductTestStandardDTO> productTestStandardDTOs { get; set; }
    }
}
