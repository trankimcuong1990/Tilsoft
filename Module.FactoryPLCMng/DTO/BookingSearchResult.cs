using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng.DTO
{
    public class BookingSearchResult
    {
        public int BookingID { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string ConfirmationNo { get; set; }
        public string PoDName { get; set; }
        public string PoLname { get; set; }
        public string ForwarderNM { get; set; }
        public string Season { get; set; }
    }
}
