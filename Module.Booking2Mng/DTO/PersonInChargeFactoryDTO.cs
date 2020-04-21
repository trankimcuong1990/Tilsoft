using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class PersonInChargeFactoryDTO
    {
        public long KeyID { get; set; }
        public string FullName { get; set; }
        public string JobLevel { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string ResponsibleFor { get; set; }
        public int SupplierID { get; set; }
        public string SupplierUD { get; set; }
        public int? UserID { get; set; }
    }
}
