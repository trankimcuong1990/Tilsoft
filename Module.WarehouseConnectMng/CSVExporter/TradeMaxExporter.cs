using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.WarehouseConnectMng.DTO;
using System.IO;

namespace Module.WarehouseConnectMng.CSVExporter
{
    public class TradeMaxExporter : IExporter
    {
        public byte[] ExportCSV(string path, List<ProductDTO> data)
        {
            StringBuilder sb = new StringBuilder();
            string separator = ";";

            sb.AppendLine("artno" + separator + "description" + separator + "quantity_available");
            foreach (DTO.ProductDTO dtoItem in data)
            {
                sb.AppendLine(dtoItem.ArticleCode + separator + dtoItem.ModelNM + " (" + dtoItem.MaterialColorNM + ")" + separator + dtoItem.StockQnt.ToString());
            }

            //return the byte[]
            return (new System.Text.UTF8Encoding()).GetBytes(sb.ToString());
        }

        public string FileExtension()
        {
            return ".csv";
        }
    }
}
