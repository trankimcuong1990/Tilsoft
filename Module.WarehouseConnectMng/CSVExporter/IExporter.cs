using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseConnectMng.CSVExporter
{
    interface IExporter
    {
        byte[] ExportCSV(string path, List<DTO.ProductDTO> data);
        string FileExtension();
    }
}
