using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class SearchFormData
    {
        public List<DTO.ProductionItemSearchResult> Data { get; set; }

        public List<DTO.FactoryWarehouseSearchResultDTO> FactoryWarehouseSearchResults { get; set; }

        public SearchFormData()
        {
            Data = new List<ProductionItemSearchResult>();
            FactoryWarehouseSearchResults = new List<FactoryWarehouseSearchResultDTO>();
        }
    }
}
