using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.TaskStatus> TaskStatuses { get; set; }
        public List<Support.DTO.User> Users { get; set; }
    }
}
