using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AgendaMng.DTO
{
    public class EditFormData
    {
        public DTO.AppointmentDTO Data { get; set; }
        public List<EmployeeDepartmentDTO> employeeDepartmentDTOs { get; set; }
    }
}
