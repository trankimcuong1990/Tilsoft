using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.EmployeeMng.DTO
{
    public class SearchFormData
    {
        public List<Module.EmployeeMng.DTO.EmployeeSearchResult> Data { get; set; }
        public int TotalAccount { get; set; }
        public Module.EmployeeMng.DTO.TilsoftUsage TilsoftUsage { get; set; }
    }
}
