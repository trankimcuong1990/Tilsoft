using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class PIFormContainerDTO
    {
        public List<DTO.SaleOrderMng.SaleOrderSearch> Data { get; set; }
        public List<DTO.SaleOrderMng.OrderType> OrderTypes { get; set; }

        public DTO.SaleOrderMng.LinkInfo LinkInfos { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.SaleOrderMng.SaleOrderStatusDTO> saleOrderStatusDTOs { get; set; }
    }
}
