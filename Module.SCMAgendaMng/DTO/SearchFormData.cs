using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class SearchFormData
    {
        public List<DTO.SCMAppointmentSearchResultDTO> Data { get; set; }
        public List<DTO.SCMAppointmentUserDTO> SCMAppointmentUserDTOs { get; set; }
    }
}
