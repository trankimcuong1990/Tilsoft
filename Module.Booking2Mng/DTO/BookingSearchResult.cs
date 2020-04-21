using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class BookingSearchResult
    {
        public int BookingID { get; set; }
        public bool? IsConfirmed { get; set; }
        public string Season { get; set; }
        public string BookingUD { get; set; }
        public string BookingDate { get; set; }
        public string ConfirmationNo { get; set; }
        public string BLNo { get; set; }
        public string SupplierNM { get; set; }
        public string ForwarderNM { get; set; }
        public string PoDName { get; set; }
        public string PoLname { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string MovementTermNM { get; set; }
        public string PlaceOfReceipt { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string ShipedDate { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string ETA2 { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string FactoryUD { get; set; }

        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }

        public int CreatedBy { get; set; }
        public string Creator2Name { get; set; }

        public int UpdatedBy { get; set; }
        public string Updator2Name { get; set; }
    }
}
