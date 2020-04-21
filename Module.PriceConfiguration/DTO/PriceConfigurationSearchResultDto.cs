using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceConfiguration.DTO
{
    public class PriceConfigurationSearchResultDto
    {
        public int Id { get; set; }
        public string Season { get; set; }
        public int? UpdatedBy { get; set; }
        public string FullName { get; set; }
        public string EmployeeName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
