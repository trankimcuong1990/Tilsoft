using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SampleMng
{
    public class SampleSearchResult
    {
        public int SampleID { get; set; }

        public string SampleUD { get; set; }

        public string Season { get; set; }

        public string ClientUD { get; set; }

        public string RequestTypeNM { get; set; }

        public string PurposeNM { get; set; }

        public string TransportTypeNM { get; set; }

        public string StatusNM { get; set; }

        public string Deadline { get; set; }

        public string CreatorName { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDate { get; set; }

        public string StatusUpdatorName { get; set; }

        public string StatusUpdatedDate { get; set; }

        public int? ClientID { get; set; }

        public int? SampleStatusID { get; set; }

        public int? RequestTypeID { get; set; }

        public int? PurposeID { get; set; }

        public int? TransportTypeID { get; set; }

    }
}