using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class DataContainer
    {
        public FactoryOrder FactoryOrderData { get; set; }
        public List<DTO.Support.User> Users { get; set; }
        public List<RateOrder> RateOrder { get; set; }
        //public List<DTO.Support.Factory> Factories { get; set; }

        public List<DTO.Support.SupplyChainPerson> SupplyChainPersons { get; set; }
        public List<DTO.Support.OrderType> OrderTypes { get; set; }
        public List<DTO.Support.ItemStandard> ItemStandards { get; set; }
        public List<DTO.Support.TestSamplingOption> TestSamplingOptions { get; set; }
        public List<DTO.Support.LabelingOption> LabelingOptions { get; set; }
        public List<DTO.Support.PackagingOption> PackagingOptions { get; set; }
        public List<DTO.Support.InspectionOption> InspectionOptions { get; set; }
        public List<DTO.Support.ShipmentToOption> ShipmentToOptions { get; set; }
        public List<DTO.Support.ShipmentTypeOption> ShipmentTypeOptions { get; set; }
        public List<DTO.FactoryOrderMng.FactoryListByStatus> Factories { get; set; }
    }
}
