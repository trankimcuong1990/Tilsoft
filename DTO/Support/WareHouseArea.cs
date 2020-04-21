using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class WareHouseArea
    {
        public int WareHouseAreaID { get; set; }
        public int WareHouseID { get; set; }
        public string AreaCode { get; set; }
        public string Description { get; set; }
    }
}