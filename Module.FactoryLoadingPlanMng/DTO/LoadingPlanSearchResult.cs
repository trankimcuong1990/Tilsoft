using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DTO
{
    public class LoadingPlanSearchResult
    {
        public int LoadingPlanID { get; set; }
        public bool? IsLoaded { get; set; }
        public bool? IsShipped { get; set; }
        public bool? IsConfirmed { get; set; }
        public string LoadingPlanUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ContainerRefNo { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string ControllerName { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string LoadingDate { get; set; }
        public string ShippedDate { get; set; }
        public string ShipmentDate { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }

        public bool HasInvoice { get; set; }
    }
}
