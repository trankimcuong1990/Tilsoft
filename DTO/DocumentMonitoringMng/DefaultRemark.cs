using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DocumentMonitoringMng
{
    public class DefaultRemark
    {
        public int? ConstantEntryID { get; set; }

        public int? DefaultRemarkID { get; set; }

        public string DefaultRemarkNM { get; set; }

        public int? DisplayOrder { get; set; }
    }
}