using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryPaymentMng
{
    public class EditData
    {
        public DTO.FactoryPaymentMng.FactoryPayment FactoryPayment { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.Factory> Factories { get; set; }
        public List<DTO.Support.Supplier> Suppliers { get; set; }
    }
}
