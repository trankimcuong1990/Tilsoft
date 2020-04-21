using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientCityMng
{
    public class ClientCitySearchResult
    {
        public int ClientCityID { get; set; }
        public string ClientCityUD { get; set; }
        public string ClientCityNM { get; set; }
        public string ClientCountryNM { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ClientCountryID { get; set; }
        public List<DTO.ClientCityMng.ClientCountry> ClientCountrys { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public string CreatedDateFormated
        {
            get
            {
                if (CreatedDate.HasValue)
                    return CreatedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }
        public string UpdatedDateFormated
        {
            get
            {
                if (UpdatedDate.HasValue)
                    return UpdatedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }
    }
}
