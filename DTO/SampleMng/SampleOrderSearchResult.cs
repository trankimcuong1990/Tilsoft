using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SampleMng
{
    public class SampleOrderSearchResult
    {
        public int SampleOrderID { get; set; }

        public string SampleOrderUD { get; set; }

        public string Season { get; set; }

        public string ClientUD { get; set; }

        public string PurposeNM { get; set; }

        public string TransportTypeNM { get; set; }

        public string HardDeadline { get; set; }

        public string Deadline { get; set; }

        public string Remark { get; set; }

        public string CreatorName { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDate { get; set; }

        public int? ClientID { get; set; }

        public int? TransportTypeID { get; set; }

        public int? PurposeID { get; set; }
        public int SampleOrderStatusID { get; set; }
        public string SampleOrderStatusNM { get; set; }
        public string StatusUpdatorName { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string Destination { get; set; }
    }
}