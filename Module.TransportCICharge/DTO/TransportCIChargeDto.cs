namespace Module.TransportCICharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransportCIChargeDto
    {
        public int TransportCIChargeID { get; set; }
        public string EurofarInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public int VATRate { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }

        /**
         * Client
         */
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }

        /**
         * Booking
         */
        public int BookingID { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string ETD { get; set; }

        /**
         * Currency
         */
        public int CurrencyID { get; set; }
        public string CurrencyNM { get; set; }

        /**
         * Payment Term
         */
        public int PaymentTermID { get; set; }
        public string PaymentTermUD { get; set; }
        public string PaymentTermNM { get; set; }

        /**
         * Create User
         */
        public int CreatedBy { get; set; }
        public string CreateFullName { get; set; }
        public string CreateEmployeeNM { get; set; }

        /**
         * Update User
         */
        public int UpdatedBy { get; set; }
        public string UpdateFullName { get; set; }
        public string UpdateEmployeeNM { get; set; }

        public List<DTO.TransportCIChargeDetailDto> TransportCIChargeDetail { get; set; }
    }
}
