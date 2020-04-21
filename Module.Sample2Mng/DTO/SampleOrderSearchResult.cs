﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleOrderSearchResult
    {
        public int SampleOrderID { get; set; }
        public string SampleOrderUD { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string PurposeNM { get; set; }
        public string TransportTypeNM { get; set; }
        public string SampleOrderStatusNM { get; set; }
        public string Destination { get; set; }
        public string HardDeadline { get; set; }
        public string Deadline { get; set; }
        public string Remark { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string StatusUpdatorName { get; set; }
        public string StatusUpdatedDate { get; set; }
        public int? SampleOrderStatusID { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public int? SaleID { get; set; }
        public string SaleNM { get; set; }
        public string RankName { get; set; }
        public Nullable<int> SampleProductTotalNumber { get; set; }
        public Nullable<int> SampleProductNewDevelopmentNumber { get; set; }
        public Nullable<int> SampleProductNoNewDevelopmentNumber { get; set; }
        public Nullable<int> SampleProductTableFinishNumber { get; set; }
        public Nullable<int> SampleProductTableInProgressNumber { get; set; }
        public Nullable<int> SampleProductdFromExistModelNumber { get; set; }
    }
}
