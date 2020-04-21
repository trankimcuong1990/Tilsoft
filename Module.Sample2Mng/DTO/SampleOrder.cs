﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Module.Sample2Mng.DTO
{
    public class SampleOrder
    {
        public int SampleOrderID { get; set; }
        public string SampleOrderUD { get; set; }

        [Required]
        public string Season { get; set; }

        [Required]
        public int? ClientID { get; set; }

        public string HardDeadline { get; set; }
        public string Deadline { get; set; }
        public int? TransportTypeID { get; set; }
        public int? PurposeID { get; set; }
        public int? SampleOrderStatusID { get; set; }
        public string Destination { get; set; }
        public string Remark { get; set; }
        public string NotifyEmail { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string StatusUpdatorName { get; set; }
        public string ClientUD { get; set; }
        public string SampleOrderStatusNM { get; set; }
        public string TransportTypeNM { get; set; }
        public string PurposeNM { get; set; }
        public string ShipmentDetail { get; set; }
        public string RankName { get; set; }

        public List<DTO.SampleProduct> SampleProducts { get; set; }
        public List<DTO.SampleMonitor> SampleMonitors { get; set; }
    }
}
