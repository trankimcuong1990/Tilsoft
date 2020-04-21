namespace Module.OutsourceRpt.DTO
{
    using System.Collections.Generic;

    public class SearchFormDTO
    {
        public List<OutsourceWorkOrderDTO> OutsourceWorkOrder { get; set; }
        public List<OutsourceProductionItemDTO> OutsourceProductionItem { get; set; }
        public List<OutsourceDocumentNoteDTO> OutsourceDocumentNote { get; set; }

        public SearchFormDTO()
        {
            OutsourceWorkOrder = new List<OutsourceWorkOrderDTO>();
            OutsourceProductionItem = new List<OutsourceProductionItemDTO>();
            OutsourceDocumentNote = new List<OutsourceDocumentNoteDTO>();
        }
    }
}
