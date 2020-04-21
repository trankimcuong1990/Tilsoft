using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionColorMng.DTO
{
    public class EditFormData
    {
        public DTO.CushionColor Data { get; set; }
        public List<DTO.Season> Seasons { get; set; }
        public List<DTO.CushionType> CushionTypes { get; set; }
    }
}
