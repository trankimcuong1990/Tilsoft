using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LabelingPackagingMng
{
    public class SupportData
    {
        public List<SaleOrder> SaleOrders;
        public List<Client> Clients;
        public List<PackagingMethod> PackagingMethods;
        public List<SaleOrderDetail> SaleOrderDetails;
        public List<PackagingMethod> PackingMethods;
        
    }
}
