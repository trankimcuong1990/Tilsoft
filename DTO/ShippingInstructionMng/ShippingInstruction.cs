using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ShippingInstructionMng
{
    public class ShippingInstruction
    {
        public int ShippingInstructionID { get; set; }

        public int? ClientID { get; set; }

        public int? PODID { get; set; }

        public string PODRemark { get; set; }

        public int? CountryID { get; set; }

        public string CountryNM { get; set; }

        public int? ForwarderID { get; set; }

        public string ForwarderInfo { get; set; }

        public int? NotifyTypeID { get; set; }

        public string NotifyInfo { get; set; }

        public int? ConsigneeTypeID { get; set; }

        public string ConsigneeInfo { get; set; }

        public bool? IsConfirmed { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string Remark { get; set; }

        public int? ConfirmedBy { get; set; }

        public string ConfirmedDate { get; set; }

        public string ClientPONo { get; set; }

        public string DocumentaryCreditNo { get; set; }

        public string ContactNo { get; set; }

        public string Priority { get; set; }

        public bool? IsDefault { get; set; }
        public bool? IsSample { get; set; }

        public bool? IsLC { get; set; }

        public string ConcurrencyFlag_String { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string PoDName { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string ClientCountryNM { get; set; }

        public string ClientAddress { get; set; }

        public string ForwarderNM { get; set; }

        public string ConsigneeTypeNM { get; set; }

        public string NotifyTypeNM { get; set; }

        public string UpdatorName { get; set; }

        public string ConfirmerName { get; set; }

        public bool? IsTelex { get; set; }

        public string SubmissionNL { get; set; }
    }
}