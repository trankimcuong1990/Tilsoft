using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class PersonInChargeDTO
    {
        public long KeyID { get; set; }
        public string Email { get; set; }
        public string ResponsibleFor { get; set; }
        public int SupplierID { get; set; }
        public string SupplierUD { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}
