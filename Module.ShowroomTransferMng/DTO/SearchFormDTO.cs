using System.Collections.Generic;

namespace Module.ShowroomTransferMng.DTO
{
    public class SearchFormDTO
    {
        public List<ShowroomTransferSearchResultDTO> Data { get; set; }

        public SearchFormDTO()
        {
            Data = new List<ShowroomTransferSearchResultDTO>();
        }
    }
}
