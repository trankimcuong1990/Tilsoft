using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng.DTO
{
    public class SearchForm
    {
        public List<WorkCenterSearchResultDTO> Data { get; set; }

        public List<WorkCenterDetailSearchResultDTO> DetailData { get; set; }

        public List<FactoryWarehouseDTO> FactoryWarehouses { get; set; }

        public SearchForm()
        {
            Data = new List<WorkCenterSearchResultDTO>();
            DetailData = new List<WorkCenterDetailSearchResultDTO>();
            FactoryWarehouses = new List<FactoryWarehouseDTO>();
        }
    }
}
