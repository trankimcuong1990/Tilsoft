using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BookingMng
{
    public partial class BookingMngEntities
    {
        public BookingMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
