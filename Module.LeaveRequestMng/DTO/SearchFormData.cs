using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.LeaveRequestMng.DTO
{
    public class SearchFormData
    {
        public List<Module.LeaveRequestMng.DTO.LeaveRequestSearchResult> Data { get; set; }
    }
}
