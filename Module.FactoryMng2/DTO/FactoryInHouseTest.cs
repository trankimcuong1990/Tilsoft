using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryMng2.DTO
{
    public class FactoryInHouseTest
    {
        public int FactoryInHouseTestID { get; set; }
        public int? FactoryID { get; set; }
        public string Description { get; set; }
    }
}
