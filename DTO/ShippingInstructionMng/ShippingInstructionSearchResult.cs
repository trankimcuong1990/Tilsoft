using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ShippingInstructionMng
{
    public class ShippingInstructionSearchResult
    {
        public int? ShippingInstructionID { get; set; }

        public bool? IsConfirmed { get; set; }

        public bool? IsDefault { get; set; }
        public bool? IsSample { get; set; }

        public bool? IsLC { get; set; }
        public string Priority { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string ForwarderNM { get; set; }

        public string PoDName { get; set; }

        public string ForwarderInfo { get; set; }

        public string NotifyTypeNM { get; set; }

        public string NotifyInfo { get; set; }

        public string ConsigneeTypeNM { get; set; }

        public string ConsigneeInfo { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDate { get; set; }

        public string ConfirmerName { get; set; }

        public string ConfirmedDate { get; set; }

        public bool? IsTelex { get; set; }
    }
}