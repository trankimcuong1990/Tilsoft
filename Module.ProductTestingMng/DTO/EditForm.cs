using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductTestingMng.DTO
{
    public class EditForm
    {
        public DTO.ProductTestingDTO Data { get; set; }
        public List<DTO.SupportProductTestStandard> supportProductTestStandards { get; set; }
    }
}
