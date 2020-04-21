namespace Module.ProductionTeam.DTO
{
    using System.Collections.Generic;

    public class SearchData
    {
        public List<DTO.ProductionTeamSearchDto> Data { get; set; }
        public List<Module.Support.DTO.WorkCenter> WorkCenters { get; set; }
    }
}
