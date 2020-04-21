using System.Collections.Generic;

namespace Module.PODMng.DTO
{
    public class EditFormData
    {
        public POD Data { get; set; }
        public List<ListClientCountryDTO> ClientCountries { get; set; }
    }
}
