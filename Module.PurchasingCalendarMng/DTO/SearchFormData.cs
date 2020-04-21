using System.Collections.Generic;

namespace Module.PurchasingCalendarMng.DTO
{
    public class SearchFormData
    {
        public List<PurchasingCalendarAppointmentSearchResultDTO> Data { get; set; }
        public List<PurchasingCalendarUserDTO> PurchasingCalendarUserDTOs { get; set; }
    }
}
