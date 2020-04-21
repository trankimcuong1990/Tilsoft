using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class WareHouse
    {
        public int WareHouseID { get; set; }
        public string WareHouseUD { get; set; }
        public string WareHouseNM { get; set; }
    }
}