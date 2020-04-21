using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SampleMng
{
    public class SampleProgress
    {
        public int SampleProgressID { get; set; }

        public int SampleProductID { get; set; }
        public int Version { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string Remark { get; set; }
        public List<SampleProgressImage> SampleProgressImages { get; set; }
        public List<SampleRemark> SampleRemarks { get; set; }

    }
}