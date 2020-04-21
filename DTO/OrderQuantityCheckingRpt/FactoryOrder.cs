using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.OrderQuantityCheckingRpt
{
    public class FactoryOrder
    {
        public string FactoryOrderUD { get; set; }

        public string FactoryUD { get; set; }

        public int OrderQnt { get; set; }

    }
}