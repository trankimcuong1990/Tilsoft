using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BookingMng
{
    public class InitFormData
    {
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.Client> Clients { get; set; }
        public List<DTO.Support.Supplier> Suppliers {get;set;}
    }
}
