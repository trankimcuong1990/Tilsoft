﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleQARemark
    {
        public int SampleQARemarkID { get; set; }
        public int? SampleProductID { get; set; }
        public string Remark { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public List<DTO.SampleQARemarkImage> SampleQARemarkImages { get; set; }
    }
}
