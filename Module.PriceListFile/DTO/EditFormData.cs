using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceListFile.DTO
{
    public class EditFormData
    {
        public PriceListFile Data { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
    }
}
