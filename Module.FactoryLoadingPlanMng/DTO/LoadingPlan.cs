using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DTO
{
    public class LoadingPlan
    {
        public int LoadingPlanID { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ContainerRefNo { get; set; }
        public int? FactoryID { get; set; }
        public string ControllerName { get; set; }
        public int? BookingID { get; set; }
        public string ContainerNo { get; set; }
        public int? ContainerTypeID { get; set; }
        public string SealNo { get; set; }
        public string Description { get; set; }
        public bool? IsLoaded { get; set; }
        public string LoadingDate { get; set; }
        public bool? IsShipped { get; set; }
        public string ShippedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public string ShipmentDate { get; set; }
        public string Remark { get; set; }
        public string ProductPicture1 { get; set; }
        public string ProductPicture2 { get; set; }
        public string ContainerPicture1 { get; set; }
        public string ContainerPicture2 { get; set; }
        public string ConfirmedDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ConcurrencyFlag { get; set; }
        public int? ParentID { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string SenderName { get; set; }
        public string FactoryUD { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string ConfirmationNo { get; set; }
        public string PoDName { get; set; }
        public string PoLname { get; set; }
        public string ClientUD { get; set; }
        public string Season { get; set; }
        public string CutOffDate { get; set; }
        public string Feeder { get; set; }
        public string Vessel { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string ForwarderNM { get; set; }
        public string LoadingStatus { get; set; }
        public string ProductPicture1_DisplayUrl { get; set; }
        public string ProductPicture2_DisplayUrl { get; set; }
        public string ContainerPicture1_DisplayUrl { get; set; }
        public string ContainerPicture2_DisplayUrl { get; set; }
        public string ProductPicture1_OriginalUrl { get; set; }
        public string ProductPicture2_OriginalUrl { get; set; }
        public string ContainerPicture1_OriginalUrl { get; set; }
        public string ContainerPicture2_OriginalUrl { get; set; }
        public string ParentLoadingPlanUD { get; set; }
        public string ParentContainerNo { get; set; }
        public string ParentContainerTypeNM { get; set; }
        public string ParentSealNo { get; set; }

        public string ParentShipmentDate { get; set; }
        public string ParentControllerName { get; set; }
        public string ParentLoadingDate { get; set; }

        public bool HasInvoice { get; set; }

        public List<LoadingPlanDetail> LoadingPlanDetails { get; set; }
        public List<LoadingPlanSparepartDetail> LoadingPlanSparepartDetails { get; set; }


        public bool ProductPicture1_HasChange { get; set; }
        public bool ProductPicture2_HasChange { get; set; }
        public bool ContainerPicture1_HasChange { get; set; }
        public bool ContainerPicture2_HasChange { get; set; }
        public string ProductPicture1_NewFile { get; set; }
        public string ProductPicture2_NewFile { get; set; }
        public string ContainerPicture1_NewFile { get; set; }
        public string ContainerPicture2_NewFile { get; set; }
    }
}
