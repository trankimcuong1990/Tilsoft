using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserMng
{
    public class EditFormData
    {
        public UserProfile Data { get; set; }
        public List<Support.UserGroup> UserGroups { get; set; }
        public List<Support.Office> Offices { get; set; }
    }
}
