using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RAPVNRpt.DTO
{
    public class EditForm
    {
        public List<Support.DTO.Season> Season { get; set; }
        public List<Support.DTO.Factory> Factory { get; set; }
    }
}
