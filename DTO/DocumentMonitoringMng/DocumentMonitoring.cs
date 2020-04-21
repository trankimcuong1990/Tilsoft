using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DocumentMonitoringMng
{
    public class DocumentMonitoring
    {
        public int? DocumentMonitoringID { get; set; }

        public int? PurchasingInvoiceID { get; set; }

        public string VNReceivedDate { get; set; }

        public int? VNReceivedBy { get; set; }

        public string SendToEUDate { get; set; }

        public int? SendToEUBy { get; set; }

        public int? DefaultRemarkID { get; set; }

        public string Remark { get; set; }

        public string Reference { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string InvoiceNo { get; set; }

        public string FactoryInvoiceNo { get; set; }

        public string BLNo { get; set; }

        public string ETA { get; set; }

        public string ETA2 { get; set; }

        public string ETAConfirmerName { get; set; }

        public string ETA2ConfirmerName { get; set; }

        public byte[] ConcurrencyFlag { get; set; }

        public string ConcurrencyFlag_String { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }
    }
}