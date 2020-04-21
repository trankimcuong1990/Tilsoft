using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportationServiceMng.DTO
{
    public class TransportationServiceData
    {
        public int TransportationServiceID { get; set; }
        public string TransportationServiceUD { get; set; }
        public string TransportationServiceNM { get; set; }
        public string PlateNumber { get; set; }
        public string DriverName { get; set; }
        public string MobileNumber { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
