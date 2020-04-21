using System.Collections.Generic;

namespace Module.ClientComplaint.DTO
{
    public class SearchFormData
    {
        public List<ClientComplaintSearchResult> Data { get; set; }

        public List<Support.DTO.Season> Seasons { get; set; }

        public List<Support.DTO.ConstantEntry> ComplaintTypes { get; set; }

        public List<Support.DTO.ConstantEntry> ComplaintStatuses { get; set; }

        public List<Module.Support.DTO.Saler> Sales { get; set; }


        public int TotalRows { get; set; }
    }
}
