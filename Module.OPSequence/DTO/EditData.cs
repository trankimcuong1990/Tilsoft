using System.Collections.Generic;
namespace Module.OPSequence.DTO
{
    public class EditData
    {
        public OPSequenceDto Data { get; set; }
        public List<Module.Support.DTO.WorkCenter> WorkCenters { get; set; }
    }
}
