using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PackingListMng
{
    public class InitInfo
    {

        [Display(Name = "PurchasingInvoiceID")]
        public int PurchasingInvoiceID { get; set; }
        
        [Display(Name = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [Display(Name = "InvoiceDate")]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "ShipedDate")]
        public DateTime? ShipedDate { get; set; }

        [Display(Name = "ForwarderNM")]
        public string ForwarderNM { get; set; }

        [Display(Name = "PODName")]
        public string PODName { get; set; }

        [Display(Name = "POLName")]
        public string POLName { get; set; }

        public string InvoiceDateFormated
        {
            get
            {
                if (InvoiceDate.HasValue)
                    return InvoiceDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string ShipedDateFormated
        {
            get
            {
                if (ShipedDate.HasValue)
                    return ShipedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

    }
}