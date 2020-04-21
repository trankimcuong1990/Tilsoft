using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientCityMng
{
    public class EditFormData
    {
        public DTO.ClientCityMng.ClientCity Data { get; set; }

        public List<DTO.Support.ClientCountry> ClientCountrys { get; set; }
        
    }
}
