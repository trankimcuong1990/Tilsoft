using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLocationMng.DTO
{
    public class FactoryLocation
    {
        public int LocationID { get; set; }
        public string LocationUD { get; set; }
        public string LocationNM { get; set; }

        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string CreatorName { get; set; }
        public bool? HasLink1 { get; set; }

        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasLink2 { get; set; }

        public int? ManufacturerCountryID { get; set; }
        public string ManufacturerCountryNM { get; set; }
    }
}
