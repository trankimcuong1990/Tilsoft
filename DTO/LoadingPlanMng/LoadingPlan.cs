using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LoadingPlanMng
{
    public class LoadingPlan
    {
        public int LoadingPlanID { get; set; }

        [Display(Name = "LoadingPlanUD")]
        public string LoadingPlanUD { get; set; }

        [Display(Name = "ContainerRefNo")]
        public string ContainerRefNo { get; set; }

        [Display(Name = "FactoryID")]
        public int? FactoryID { get; set; }

        [Display(Name = "ControllerName")]
        public string ControllerName { get; set; }
        public int ControllerID { get; set; }

        [Display(Name = "BookingID")]
        public int? BookingID { get; set; }

        [Display(Name = "ContainerNo")]
        public string ContainerNo { get; set; }

        [Display(Name = "ContainerTypeID")]
        public int? ContainerTypeID { get; set; }

        [Display(Name = "SealNo")]
        public string SealNo { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "IsLoaded")]
        public bool? IsLoaded { get; set; }

        [Display(Name = "LoadingDate")]
        public string LoadingDate { get; set; }

        [Display(Name = "IsShipped")]
        public bool? IsShipped { get; set; }

        [Display(Name = "ShippedDate")]
        public string ShippedDate { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "IsSent")]
        public object IsSent { get; set; }

        [Display(Name = "SentBy")]
        public int? SentBy { get; set; }

        [Display(Name = "SentDate")]
        public string SentDate { get; set; }

        [Display(Name = "ShipmentDate")]
        public string ShipmentDate { get; set; }

        [Display(Name = "ProductPicture1")]
        public string ProductPicture1 { get; set; }
        public bool ProductPicture1_HasChange { get; set; }
        public string ProductPicture1_NewFile { get; set; }
        public string ProductPicture1_DisplayUrl { get; set; }
        public string ProductPicture1_OriginalUrl { get; set; }

        [Display(Name = "ProductPicture2")]
        public string ProductPicture2 { get; set; }
        public bool ProductPicture2_HasChange { get; set; }
        public string ProductPicture2_NewFile { get; set; }
        public string ProductPicture2_DisplayUrl { get; set; }
        public string ProductPicture2_OriginalUrl { get; set; }

        [Display(Name = "ContainerPicture1")]
        public string ContainerPicture1 { get; set; }
        public bool ContainerPicture1_HasChange { get; set; }
        public string ContainerPicture1_NewFile { get; set; }
        public string ContainerPicture1_DisplayUrl { get; set; }
        public string ContainerPicture1_OriginalUrl { get; set; }

        [Display(Name = "ContainerPicture2")]
        public string ContainerPicture2 { get; set; }
        public bool ContainerPicture2_HasChange { get; set; }
        public string ContainerPicture2_NewFile { get; set; }
        public string ContainerPicture2_DisplayUrl { get; set; }
        public string ContainerPicture2_OriginalUrl { get; set; }

        [Display(Name = "ContainerPicture3")]
        public string ContainerPicture3 { get; set; }
        public bool ContainerPicture3_HasChange { get; set; }
        public string ContainerPicture3_NewFile { get; set; }
        public string ContainerPicture3_DisplayUrl { get; set; }
        public string ContainerPicture3_OriginalUrl { get; set; }

        [Display(Name = "ContainerPicture4")]
        public string ContainerPicture4 { get; set; }
        public bool ContainerPicture4_HasChange { get; set; }
        public string ContainerPicture4_NewFile { get; set; }
        public string ContainerPicture4_DisplayUrl { get; set; }
        public string ContainerPicture4_OriginalUrl { get; set; }

        [Display(Name = "ContainerPicture5")]
        public string ContainerPicture5 { get; set; }
        public bool ContainerPicture5_HasChange { get; set; }
        public string ContainerPicture5_NewFile { get; set; }
        public string ContainerPicture5_DisplayUrl { get; set; }
        public string ContainerPicture5_OriginalUrl { get; set; }

        [Display(Name = "ContainerPicture6")]
        public string ContainerPicture6 { get; set; }
        public bool ContainerPicture6_HasChange { get; set; }
        public string ContainerPicture6_NewFile { get; set; }
        public string ContainerPicture6_DisplayUrl { get; set; }
        public string ContainerPicture6_OriginalUrl { get; set; }


        [Display(Name = "MerchandisesInside1Per3ContImage")]
        public string MerchandisesInside1Per3ContImage { get; set; }
        public bool MerchandisesInside1Per3ContImage_HasChange { get; set; }
        public string MerchandisesInside1Per3ContImage_NewFile { get; set; }
        public string MerchandisesInside1Per3ContImage_DisplayUrl { get; set; }
        public string MerchandisesInside1Per3ContImage_OriginalUrl { get; set; }

        [Display(Name = "MerchandisesInside1Per2ContImage")]
        public string MerchandisesInside1Per2ContImage { get; set; }
        public bool MerchandisesInside1Per2ContImage_HasChange { get; set; }
        public string MerchandisesInside1Per2ContImage_NewFile { get; set; }
        public string MerchandisesInside1Per2ContImage_DisplayUrl { get; set; }
        public string MerchandisesInside1Per2ContImage_OriginalUrl { get; set; }

        [Display(Name = "MerchandisesInsideFullContImage")]
        public string MerchandisesInsideFullContImage { get; set; }
        public bool MerchandisesInsideFullContImage_HasChange { get; set; }
        public string MerchandisesInsideFullContImage_NewFile { get; set; }
        public string MerchandisesInsideFullContImage_DisplayUrl { get; set; }
        public string MerchandisesInsideFullContImage_OriginalUrl { get; set; }

        [Display(Name = "ContainerDoorAndLockImage")]
        public string ContainerDoorAndLockImage { get; set; }
        public bool ContainerDoorAndLockImage_HasChange { get; set; }
        public string ContainerDoorAndLockImage_NewFile { get; set; }
        public string ContainerDoorAndLockImage_DisplayUrl { get; set; }
        public string ContainerDoorAndLockImage_OriginalUrl { get; set; }

        [Display(Name = "ContainerSealImage")]
        public string ContainerSealImage { get; set; }
        public bool ContainerSealImage_HasChange { get; set; }
        public string ContainerSealImage_NewFile { get; set; }
        public string ContainerSealImage_DisplayUrl { get; set; }
        public string ContainerSealImage_OriginalUrl { get; set; }

        [Display(Name = "DryPackImage")]
        public string DryPackImage { get; set; }
        public bool DryPackImage_HasChange { get; set; }
        public string DryPackImage_NewFile { get; set; }
        public string DryPackImage_DisplayUrl { get; set; }
        public string DryPackImage_OriginalUrl { get; set; }

        [Display(Name = "ApprovedImage")]
        public string ApprovedImage { get; set; }
        public bool ApprovedImage_HasChange { get; set; }
        public string ApprovedImage_NewFile { get; set; }
        public string ApprovedImage_DisplayUrl { get; set; }
        public string ApprovedImage_OriginalUrl { get; set; }


        [Display(Name = "ConfirmedDate")]
        public string ConfirmedDate { get; set; }

        [Display(Name = "ConfirmedBy")]
        public int? ConfirmedBy { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "ConfirmerName")]
        public string ConfirmerName { get; set; }

        [Display(Name = "SenderName")]
        public string SenderName { get; set; }

        [Display(Name = "FactoryUD")]
        public string FactoryUD { get; set; }

        [Display(Name = "BookingUD")]
        public string BookingUD { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        public byte[] ConcurrencyFlag { get; set; }
        public string ConcurrencyFlag_String { get; set; }

        public string CutOffDate { get; set; }
        public string Feeder { get; set; }
        public string Vessel { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string ForwarderNM { get; set; }

        public List<LoadingPlanDetail> Details { get; set; }
        public List<LoadingPlanSparepartDetail> Spareparts { get; set; }
        public List<LoadingPlanSampleProductDetail> SampleProducts { get; set; }

        public int ParentID { get; set; }
        public string LoadingStatus { get; set; }
        public string ParentLoadingPlanUD { get; set; }
        public string ParentContainerNo { get; set; }
        public string ParentContainerTypeNM { get; set; }
        public string ParentSealNo { get; set; }
        public List<ChildLoadingPlan> ChildLoadingPlans { get; set; }

        public bool IsCreatedInvoice { get; set; }

        public string ConfirmerName2 { get; set; }
        public string UpdatorName2 { get; set; }
        public string SenderName2 { get; set; }

        public bool? IsBookingConfirmed { get; set; }
    }
}