using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseImportMng
{
    public class SearchFormData
    {
        public List<WarehouseImportSearchResult> Data { get; set; }
        public List<Support.WarehouseImportType> ImportTypes { get; set; }
    }
}
