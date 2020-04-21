using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AgendaMng.DTO
{
    public class SearchFormData
    {
        public List<DTO.AppointmentSearchResultDTO> Data { get; set; }
        public List<DTO.AgendaMngUser> agendaMngUsers { get; set; }
    }
}
