using System.Collections.Generic;

namespace Module.QAQCMng.DTO
{
    public class CategoryDTO
    {
        public List<CheckListDTO> CheckListDTOs { get; set; }
        public List<DefectDTO> DefectDTOs { get; set; }
    }
}
