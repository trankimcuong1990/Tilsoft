using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProfileMng.DTO
{
    public class EditFormData
    {
        public DTO.UserProfile Data { get; set; }
        public List<Support.DTO.Location> Locations { get; set; }
    }
}
