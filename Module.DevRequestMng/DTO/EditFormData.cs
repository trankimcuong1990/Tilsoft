using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestMng.DTO
{
    public class EditFormData
    {
        public DTO.DevRequest Data { get; set; }
        public DTO.DevRequestHistory HistoryData { get; set; }

        public List<Support.DTO.DevRequestType> DevRequestTypes { get; set; }
        public List<Support.DTO.DevRequestProject> DevRequestProjects { get; set; }
        public List<Support.DTO.DevRequestPriority> DevRequestPriorities { get; set; }
        public List<DTO.Requester> Requesters { get; set; }

        public List<DTO.DevRequestPerson> DevRequestPersons { get; set; }
        public List<Support.DTO.DevRequestStatus> DevRequestStatuses { get; set; }
    }
}
