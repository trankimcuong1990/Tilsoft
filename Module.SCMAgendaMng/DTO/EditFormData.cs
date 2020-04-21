using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class EditFormData
    {
        public DTO.SCMAppointmentDTO Data { get; set; }
        public List<EmployeeDepartmentDTO> employeeDepartmentDTOs { get; set; }
    }
}
