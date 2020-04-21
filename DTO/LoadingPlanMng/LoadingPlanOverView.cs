﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LoadingPlanMng
{
   public class LoadingPlanOverView
    {
        public int LoadingPlanID { get; set; }

        public string LoadingPlanUD { get; set; }

        public string ContainerRefNo { get; set; }

        public int? FactoryID { get; set; }

        public string ControllerName { get; set; }

        public int ControllerID { get; set; }

        public int? BookingID { get; set; }

        public string ContainerNo { get; set; }

        public int? ContainerTypeID { get; set; }

        public string ContainerTypeNM { get; set; }

        public string SealNo { get; set; }

        public string Description { get; set; }

        public bool? IsLoaded { get; set; }

        public string LoadingDate { get; set; }

        public bool? IsShipped { get; set; }

        public string ShippedDate { get; set; }

        public bool? IsConfirmed { get; set; }

        public object IsSent { get; set; }

        public int? SentBy { get; set; }

        public string SentDate { get; set; }

        public string ShipmentDate { get; set; }

        public string ProductPicture1 { get; set; }
        public bool ProductPicture1_HasChange { get; set; }
        public string ProductPicture1_NewFile { get; set; }
        public string ProductPicture1_DisplayUrl { get; set; }
        public string ProductPicture1_OriginalUrl { get; set; }

        public string ProductPicture2 { get; set; }
        public bool ProductPicture2_HasChange { get; set; }
        public string ProductPicture2_NewFile { get; set; }
        public string ProductPicture2_DisplayUrl { get; set; }
        public string ProductPicture2_OriginalUrl { get; set; }

        public string ContainerPicture1 { get; set; }
        public bool ContainerPicture1_HasChange { get; set; }
        public string ContainerPicture1_NewFile { get; set; }
        public string ContainerPicture1_DisplayUrl { get; set; }
        public string ContainerPicture1_OriginalUrl { get; set; }

        public string ContainerPicture2 { get; set; }
        public bool ContainerPicture2_HasChange { get; set; }
        public string ContainerPicture2_NewFile { get; set; }
        public string ContainerPicture2_DisplayUrl { get; set; }
        public string ContainerPicture2_OriginalUrl { get; set; }

        public string ContainerPicture3 { get; set; }
        public bool ContainerPicture3_HasChange { get; set; }
        public string ContainerPicture3_NewFile { get; set; }
        public string ContainerPicture3_DisplayUrl { get; set; }
        public string ContainerPicture3_OriginalUrl { get; set; }

        public string ContainerPicture4 { get; set; }
        public bool ContainerPicture4_HasChange { get; set; }
        public string ContainerPicture4_NewFile { get; set; }
        public string ContainerPicture4_DisplayUrl { get; set; }
        public string ContainerPicture4_OriginalUrl { get; set; }

        public string ContainerPicture5 { get; set; }
        public bool ContainerPicture5_HasChange { get; set; }
        public string ContainerPicture5_NewFile { get; set; }
        public string ContainerPicture5_DisplayUrl { get; set; }
        public string ContainerPicture5_OriginalUrl { get; set; }

        public string ContainerPicture6 { get; set; }
        public bool ContainerPicture6_HasChange { get; set; }
        public string ContainerPicture6_NewFile { get; set; }
        public string ContainerPicture6_DisplayUrl { get; set; }
        public string ContainerPicture6_OriginalUrl { get; set; }


        public string MerchandisesInside1Per3ContImage { get; set; }
        public bool MerchandisesInside1Per3ContImage_HasChange { get; set; }
        public string MerchandisesInside1Per3ContImage_NewFile { get; set; }
        public string MerchandisesInside1Per3ContImage_DisplayUrl { get; set; }
        public string MerchandisesInside1Per3ContImage_OriginalUrl { get; set; }

        public string MerchandisesInside1Per2ContImage { get; set; }
        public bool MerchandisesInside1Per2ContImage_HasChange { get; set; }
        public string MerchandisesInside1Per2ContImage_NewFile { get; set; }
        public string MerchandisesInside1Per2ContImage_DisplayUrl { get; set; }
        public string MerchandisesInside1Per2ContImage_OriginalUrl { get; set; }

        public string MerchandisesInsideFullContImage { get; set; }
        public bool MerchandisesInsideFullContImage_HasChange { get; set; }
        public string MerchandisesInsideFullContImage_NewFile { get; set; }
        public string MerchandisesInsideFullContImage_DisplayUrl { get; set; }
        public string MerchandisesInsideFullContImage_OriginalUrl { get; set; }

        public string ContainerDoorAndLockImage { get; set; }
        public bool ContainerDoorAndLockImage_HasChange { get; set; }
        public string ContainerDoorAndLockImage_NewFile { get; set; }
        public string ContainerDoorAndLockImage_DisplayUrl { get; set; }
        public string ContainerDoorAndLockImage_OriginalUrl { get; set; }

        public string ContainerSealImage { get; set; }
        public bool ContainerSealImage_HasChange { get; set; }
        public string ContainerSealImage_NewFile { get; set; }
        public string ContainerSealImage_DisplayUrl { get; set; }
        public string ContainerSealImage_OriginalUrl { get; set; }

        public string DryPackImage { get; set; }
        public bool DryPackImage_HasChange { get; set; }
        public string DryPackImage_NewFile { get; set; }
        public string DryPackImage_DisplayUrl { get; set; }
        public string DryPackImage_OriginalUrl { get; set; }

        public string ApprovedImage { get; set; }
        public bool ApprovedImage_HasChange { get; set; }
        public string ApprovedImage_NewFile { get; set; }
        public string ApprovedImage_DisplayUrl { get; set; }
        public string ApprovedImage_OriginalUrl { get; set; }


        public string ConfirmedDate { get; set; }

        public int? ConfirmedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string UpdatorName { get; set; }

        public string ConfirmerName { get; set; }

        public string SenderName { get; set; }

        public string FactoryUD { get; set; }

        public string BookingUD { get; set; }

        public string BLNo { get; set; }

        public int? ClientID { get; set; }

        public string ClientUD { get; set; }

        public byte[] ConcurrencyFlag { get; set; }
        public string ConcurrencyFlag_String { get; set; }

        public string CutOffDate { get; set; }
        public string Feeder { get; set; }
        public string Vessel { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string ForwarderNM { get; set; }

        public int ParentID { get; set; }
        public string LoadingStatus { get; set; }
        public string ParentLoadingPlanUD { get; set; }
        public string ParentContainerNo { get; set; }
        public string ParentContainerTypeNM { get; set; }
        public string ParentSealNo { get; set; }
   

        public bool IsCreatedInvoice { get; set; }
        public string ConfirmerName2 { get; set; }
        public string UpdatorName2 { get; set; }
        public string SenderName2 { get; set; }


        public List<LoadingPlanDetailOverview> Details { get; set; }
        public List<LoadingPlanSparepartDetailOverview> Spareparts { get; set; }
        public List<ChildLoadingPlanOverView> ChildLoadingPlans { get; set; }
    }
}
