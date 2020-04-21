using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DocumentMonitoringMng
{
    public class DocumentMonitoringSearch
    {
        public int? DocumentMonitoringID { get; set; }

        public string VNReceivedDate { get; set; }

        public string VNReceiverName { get; set; }

        public string SendToEUDate { get; set; }

        public string SenderToEUName { get; set; }

        public string Remark { get; set; }

        public string Reference { get; set; }

        public string CreatorName { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDate { get; set; }

        public string InvoiceNo { get; set; }

        public string FactoryInvoiceNo { get; set; }

        public string BLNo { get; set; }

        public string ETA { get; set; }

        public string ETA2 { get; set; }

        public string ETAConfirmerName { get; set; }

        public string ETA2ConfirmerName { get; set; }

        public string BLFileStatus { get; set; }

        public string Season { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public string VNReceiverName2 { get; set; }

        public string SenderToEUName2 { get; set; }

        public string CreatorName2 { get; set; }

        public string UpdatorName2 { get; set; }

        public string ETAConfirmerName2 { get; set; }

        public string ETA2ConfirmerName2 { get; set; }

        public int VNReceivedBy { get; set; }

        public int SendToEUBy { get; set; }

        public int ETAConfirmedBy { get; set; }

        public int ETA2ConfirmedBy { get; set; }

        public int IsEdit { get; set; }
    }
}