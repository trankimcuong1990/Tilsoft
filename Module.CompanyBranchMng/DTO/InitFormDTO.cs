using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng.DTO
{
    public class InitFormDTO
    {
        public List<LocationDTO> Locations { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }

        public InitFormDTO()
        {
            Locations = new List<LocationDTO>();
            Factories = new List<Support.DTO.Factory>();
        }
    }
}
