using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class ModelFixPriceConfiguration
    {
        public int ModelFixPriceConfigurationID { get; set; }      
        public string Season { get; set; }
        public int? MaterialTypeID { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? EURAmount { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public string MaterialTypeNM { get; set; }
    }
}
