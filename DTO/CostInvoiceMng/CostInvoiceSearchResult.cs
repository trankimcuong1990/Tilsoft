using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CostInvoiceMng
{
    public class CostInvoiceSearchResult
    {
        public int? CostInvoiceID { get; set; }

        public string InvoiceNo { get; set; }

        public string InvoiceRefNo { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string Currency { get; set; }

        public string Condition { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string BLNo { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string DeliveryTermNM { get; set; }

        public string PaymentTermNM { get; set; }

        public string DeliveryTypeNM { get; set; }

        public string PaymentTypeNM { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public decimal? Amount { get; set; }

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

        public string CreatedDateFormated
        {
            get
            {
                if (CreatedDate.HasValue)
                    return CreatedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string UpdatedDateFormated
        {
            get
            {
                if (UpdatedDate.HasValue)
                    return UpdatedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

    }
}