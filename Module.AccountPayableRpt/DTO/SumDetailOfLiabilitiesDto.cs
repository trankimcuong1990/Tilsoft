using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountPayableRpt.DTO
{
    public class SumDetailOfLiabilitiesDto
    {
        public int FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<DetailOfLiabilitiesDto> DetailOfLiabilitiesDto { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string KeyRawMaterialNM { get; set; }
    }
}
