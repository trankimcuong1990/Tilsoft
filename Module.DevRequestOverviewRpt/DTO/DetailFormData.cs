using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestOverviewRpt.DTO
{
    public class DetailFormData
    {
        public List<DTO.DetailByPerson> DetailByPersons { get; set; }
        public List<DTO.PlaningByPerson> PlaningByPersons { get; set; }
        public List<DTO.ResolvedRequestByPerson> ResolvedRequestByPersons { get; set; }
    }
}
