using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseConnectMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public List<DTO.ProductDTO> GetFreeToSale(string wexJsonData, out Library.DTO.Notification notification)
        {
            return factory.GetFreeToSale(wexJsonData, out notification);
        }

        public byte[] GetFreeToSaleCsv(string wexJsonData, string exporterIdentifier, out string fileExtension, out Library.DTO.Notification notification)
        {
            return factory.GetFreeToSaleCsv(wexJsonData, exporterIdentifier, out fileExtension, out notification);
        }
    }
}
