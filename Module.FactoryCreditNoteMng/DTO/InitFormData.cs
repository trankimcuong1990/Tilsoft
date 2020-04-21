using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryCreditNoteMng.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.Supplier> Suppliers { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
    }
}
