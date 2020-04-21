using System.Collections.Generic;

namespace Module.InventoryRpt.DTO
{
    public class InitFormData
    {
        public List<FactoryWarehouseDTO> FactoryWarehouse { get; set; }
        public List<Support.DTO.ProductionTeam> ProductionTeam { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public InitFormData()
        {
            FactoryWarehouse = new List<FactoryWarehouseDTO>();
            ProductionTeam = new List<Support.DTO.ProductionTeam>();

            /// Start date: first date current date
            /// End date: last date current date
            System.DateTime currentDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            StartDate = currentDate.ToString("dd/MM/yyyy");
            EndDate = currentDate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
        }
    }
}
