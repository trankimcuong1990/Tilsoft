namespace Module.Support.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FactoryWarehouseDto
    {
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public int? CompanyID { get; set; }
        public bool? IsDefault { get; set; }
    }
}
