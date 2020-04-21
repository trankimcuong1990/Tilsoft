using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng.DTO
{
    public class EditFormDTO
    {
        public CompanyDTO Data { get; set; }
        public List<string> TimeRanges { get; set; }

        public EditFormDTO()
        {
            Data = new CompanyDTO();
        }
    }
}
