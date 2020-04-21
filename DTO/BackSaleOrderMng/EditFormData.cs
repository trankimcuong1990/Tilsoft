using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BackSaleOrderMng
{
    public class EditFormData
    {
        public BackOrder Data { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.Saler> Salers { get; set; }
        public List<DTO.Support.PaymentTerm> PaymentTerms { get; set; }
        public List<DTO.Support.DeliveryTerm> DeliveryTerms { get; set; }
        public List<DTO.Support.VATPercent> VATPercent { get; set; }
        public List<DTO.Support.Currency> Currency { get; set; }
        public List<DTO.Support.SaleOrderType> SaleOrderTypes { get; set; }
    }
}
