using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.UserMng
{
    public class FactoryAccess
    {
        public int UserFactoryAccessID { get; set; }
        public int? FactoryID { get; set; }
        public bool? CanAccess { get; set; }
        public bool? CanReceiveNotification { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
    }
}