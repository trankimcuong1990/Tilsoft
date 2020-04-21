using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.LoadingPlanMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "LoadingPlanMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                Mapper.CreateMap<LoadingPlanMng_LoadingPlanSearchResult_View, DTO.LoadingPlanMng.LoadingPlanSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SentDate, o => o.ResolveUsing(s => s.SentDate.HasValue ? s.SentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipmentDate, o => o.ResolveUsing(s => s.ShipmentDate.HasValue ? s.ShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_ProductSearchResult_View, DTO.LoadingPlanMng.ProductSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_SampleProductSearchResult_View, DTO.LoadingPlanMng.SampleProductSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_SparepartSearchResult_View, DTO.LoadingPlanMng.SparepartSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_LoadingPlanDetail_View, DTO.LoadingPlanMng.LoadingPlanDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d=>d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_LoadingPlanSampleDetail_View, DTO.LoadingPlanMng.LoadingPlanSampleProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_LoadingPlanSparepartDetail_View, DTO.LoadingPlanMng.LoadingPlanSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                Mapper.CreateMap<SupportMng_User2_View, DTO.LoadingPlanMng.User2>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_LoadingPlan_View, DTO.LoadingPlanMng.LoadingPlan>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SentDate, o => o.ResolveUsing(s => s.SentDate.HasValue ? s.SentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipmentDate, o => o.ResolveUsing(s => s.ShipmentDate.HasValue ? s.ShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Details, o => o.MapFrom(s => s.LoadingPlanMng_LoadingPlanDetail_View))
                    .ForMember(d => d.Spareparts, o => o.MapFrom(s => s.LoadingPlanMng_LoadingPlanSparepartDetail_View))
                    .ForMember(d => d.SampleProducts, o => o.MapFrom(s => s.LoadingPlanMng_LoadingPlanSampleDetail_View))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CutOffDate, o => o.ResolveUsing(s => s.CutOffDate.HasValue ? s.CutOffDate.Value.ToString("dd/MM/yyyy") : ""))

                    .ForMember(d => d.ProductPicture1_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ProductPicture1_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ProductPicture1_DisplayUrl : null)))
                    .ForMember(d => d.ProductPicture2_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ProductPicture2_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ProductPicture2_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture1_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture1_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture1_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture2_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture2_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture2_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture3_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture3_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture3_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture4_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture4_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture4_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture5_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture5_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture5_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture6_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture6_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture6_DisplayUrl : null)))
                    .ForMember(d => d.MerchandisesInside1Per3ContImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.MerchandisesInside1Per3ContImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.MerchandisesInside1Per3ContImage_DisplayUrl : null)))
                    .ForMember(d => d.MerchandisesInside1Per2ContImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.MerchandisesInside1Per2ContImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.MerchandisesInside1Per2ContImage_DisplayUrl : null)))
                    .ForMember(d => d.MerchandisesInsideFullContImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.MerchandisesInsideFullContImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.MerchandisesInsideFullContImage_DisplayUrl : null)))
                    .ForMember(d => d.ContainerDoorAndLockImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerDoorAndLockImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerDoorAndLockImage_DisplayUrl : null)))
                    .ForMember(d => d.ContainerSealImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerSealImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerSealImage_DisplayUrl : null)))
                    .ForMember(d => d.DryPackImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.DryPackImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.DryPackImage_DisplayUrl : null)))
                    .ForMember(d => d.ApprovedImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ApprovedImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ApprovedImage_DisplayUrl : null)))


                    .ForMember(d => d.ProductPicture1_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductPicture1_OriginalUrl) ? s.ProductPicture1_OriginalUrl : null)))
                    .ForMember(d => d.ProductPicture2_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductPicture2_OriginalUrl) ? s.ProductPicture2_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture1_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture1_OriginalUrl) ? s.ContainerPicture1_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture2_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture2_OriginalUrl) ? s.ContainerPicture2_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture3_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture3_OriginalUrl) ? s.ContainerPicture3_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture4_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture4_OriginalUrl) ? s.ContainerPicture4_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture5_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture5_OriginalUrl) ? s.ContainerPicture5_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture6_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture6_OriginalUrl) ? s.ContainerPicture6_OriginalUrl : null)))
                    .ForMember(d => d.MerchandisesInside1Per3ContImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.MerchandisesInside1Per3ContImage_OriginalUrl) ? s.MerchandisesInside1Per3ContImage_OriginalUrl : null)))
                    .ForMember(d => d.MerchandisesInside1Per2ContImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.MerchandisesInside1Per2ContImage_OriginalUrl) ? s.MerchandisesInside1Per2ContImage_OriginalUrl : null)))
                    .ForMember(d => d.MerchandisesInsideFullContImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.MerchandisesInsideFullContImage_OriginalUrl) ? s.MerchandisesInsideFullContImage_OriginalUrl : null)))
                    .ForMember(d => d.ContainerDoorAndLockImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerDoorAndLockImage_OriginalUrl) ? s.ContainerDoorAndLockImage_OriginalUrl : null)))
                    .ForMember(d => d.ContainerSealImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerSealImage_OriginalUrl) ? s.ContainerSealImage_OriginalUrl : null)))
                    .ForMember(d => d.DryPackImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.DryPackImage_OriginalUrl) ? s.DryPackImage_OriginalUrl : null)))
                    .ForMember(d => d.ApprovedImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ApprovedImage_OriginalUrl) ? s.ApprovedImage_OriginalUrl : null)))


                    .ForMember(d => d.ChildLoadingPlans, o => o.MapFrom(s => s.LoadingPlanMng_ChildLoadingPlan_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.LoadingPlanMng.LoadingPlan, LoadingPlan>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.LoadingDate, o => o.Ignore())
                    .ForMember(d => d.SentDate, o => o.Ignore())
                    .ForMember(d => d.ShipmentDate, o => o.Ignore())
                    .ForMember(d => d.ShippedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.LoadingPlanID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LoadingPlanMng.LoadingPlanDetail, LoadingPlanDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LoadingPlanDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LoadingPlanMng.LoadingPlanSparepartDetail, LoadingPlanSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LoadingPlanSparepartDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LoadingPlanMng.LoadingPlanSampleProductDetail, LoadingPlanSampleDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LoadingPlanSampleDetailID, o => o.Ignore());

                Mapper.CreateMap<LoadingPlanMng_ChildLoadingPlan_View, DTO.LoadingPlanMng.ChildLoadingPlan>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.SentDate, o => o.ResolveUsing(s => s.SentDate.HasValue ? s.SentDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ShipmentDate, o => o.ResolveUsing(s => s.ShipmentDate.HasValue ? s.ShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //For OverView

                Mapper.CreateMap<LoadingPlanMng_LoadingPlanDetail_OverView, DTO.LoadingPlanMng.LoadingPlanDetailOverview>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_LoadingPlanSparepartDetail_OverView, DTO.LoadingPlanMng.LoadingPlanSparepartDetailOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_LoadingPlan_OverView, DTO.LoadingPlanMng.LoadingPlanOverView>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SentDate, o => o.ResolveUsing(s => s.SentDate.HasValue ? s.SentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipmentDate, o => o.ResolveUsing(s => s.ShipmentDate.HasValue ? s.ShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Details, o => o.MapFrom(s => s.LoadingPlanMng_LoadingPlanDetail_OverView))
                    .ForMember(d => d.Spareparts, o => o.MapFrom(s => s.LoadingPlanMng_LoadingPlanSparepartDetail_OverView))
                    .ForMember(d => d.ChildLoadingPlans, o => o.MapFrom(s => s.LoadingPlanMng_ChildLoadingPlan_OverView))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CutOffDate, o => o.ResolveUsing(s => s.CutOffDate.HasValue ? s.CutOffDate.Value.ToString("dd/MM/yyyy") : ""))

                    .ForMember(d => d.ProductPicture1_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ProductPicture1_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ProductPicture1_DisplayUrl : null)))
                    .ForMember(d => d.ProductPicture2_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ProductPicture2_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ProductPicture2_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture1_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture1_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture1_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture2_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture2_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture2_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture3_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture3_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture3_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture4_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture4_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture4_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture5_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture5_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture5_DisplayUrl : null)))
                    .ForMember(d => d.ContainerPicture6_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerPicture6_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerPicture6_DisplayUrl : null)))
                    .ForMember(d => d.MerchandisesInside1Per3ContImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.MerchandisesInside1Per3ContImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.MerchandisesInside1Per3ContImage_DisplayUrl : null)))
                    .ForMember(d => d.MerchandisesInside1Per2ContImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.MerchandisesInside1Per2ContImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.MerchandisesInside1Per2ContImage_DisplayUrl : null)))
                    .ForMember(d => d.MerchandisesInsideFullContImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.MerchandisesInsideFullContImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.MerchandisesInsideFullContImage_DisplayUrl : null)))
                    .ForMember(d => d.ContainerDoorAndLockImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerDoorAndLockImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerDoorAndLockImage_DisplayUrl : null)))
                    .ForMember(d => d.ContainerSealImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ContainerSealImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ContainerSealImage_DisplayUrl : null)))
                    .ForMember(d => d.DryPackImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.DryPackImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.DryPackImage_DisplayUrl : null)))
                    .ForMember(d => d.ApprovedImage_DisplayUrl, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.ApprovedImage_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + s.ApprovedImage_DisplayUrl : null)))


                    .ForMember(d => d.ProductPicture1_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductPicture1_OriginalUrl) ? s.ProductPicture1_OriginalUrl : null)))
                    .ForMember(d => d.ProductPicture2_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductPicture2_OriginalUrl) ? s.ProductPicture2_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture1_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture1_OriginalUrl) ? s.ContainerPicture1_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture2_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture2_OriginalUrl) ? s.ContainerPicture2_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture3_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture3_OriginalUrl) ? s.ContainerPicture3_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture4_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture4_OriginalUrl) ? s.ContainerPicture4_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture5_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture5_OriginalUrl) ? s.ContainerPicture5_OriginalUrl : null)))
                    .ForMember(d => d.ContainerPicture6_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture6_OriginalUrl) ? s.ContainerPicture6_OriginalUrl : null)))
                    .ForMember(d => d.MerchandisesInside1Per3ContImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.MerchandisesInside1Per3ContImage_OriginalUrl) ? s.MerchandisesInside1Per3ContImage_OriginalUrl : null)))
                    .ForMember(d => d.MerchandisesInside1Per2ContImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.MerchandisesInside1Per2ContImage_OriginalUrl) ? s.MerchandisesInside1Per2ContImage_OriginalUrl : null)))
                    .ForMember(d => d.MerchandisesInsideFullContImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.MerchandisesInsideFullContImage_OriginalUrl) ? s.MerchandisesInsideFullContImage_OriginalUrl : null)))
                    .ForMember(d => d.ContainerDoorAndLockImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerDoorAndLockImage_OriginalUrl) ? s.ContainerDoorAndLockImage_OriginalUrl : null)))
                    .ForMember(d => d.ContainerSealImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContainerSealImage_OriginalUrl) ? s.ContainerSealImage_OriginalUrl : null)))
                    .ForMember(d => d.DryPackImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.DryPackImage_OriginalUrl) ? s.DryPackImage_OriginalUrl : null)))
                    .ForMember(d => d.ApprovedImage_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ApprovedImage_OriginalUrl) ? s.ApprovedImage_OriginalUrl : null)))

                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<LoadingPlanMng_ChildLoadingPlan_OverView, DTO.LoadingPlanMng.ChildLoadingPlanOverView>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.SentDate, o => o.ResolveUsing(s => s.SentDate.HasValue ? s.SentDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ShipmentDate, o => o.ResolveUsing(s => s.ShipmentDate.HasValue ? s.ShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.LoadingPlanMng.LoadingPlanSearchResult> DB2DTO_LoadingPlanSearchResultList(List<LoadingPlanMng_LoadingPlanSearchResult_View> dbItems)
        {
            return Mapper.Map<List<LoadingPlanMng_LoadingPlanSearchResult_View>, List<DTO.LoadingPlanMng.LoadingPlanSearchResult>>(dbItems);
        }

        public List<DTO.LoadingPlanMng.User2> DB2DTO_User2List(List<SupportMng_User2_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_User2_View>, List<DTO.LoadingPlanMng.User2>>(dbItems);
        }

        public List<DTO.LoadingPlanMng.ProductSearchResult> DB2DTO_ProductSearchResultList(List<LoadingPlanMng_ProductSearchResult_View> dbItems)
        {
            return Mapper.Map<List<LoadingPlanMng_ProductSearchResult_View>, List<DTO.LoadingPlanMng.ProductSearchResult>>(dbItems);
        }

        public List<DTO.LoadingPlanMng.SampleProductSearchResult> DB2DTO_SampleProductSearchResultList(List<LoadingPlanMng_SampleProductSearchResult_View> dbItems)
        {
            return Mapper.Map<List<LoadingPlanMng_SampleProductSearchResult_View>, List<DTO.LoadingPlanMng.SampleProductSearchResult>>(dbItems);
        }

        public List<DTO.LoadingPlanMng.SparepartSearchResult> DB2DTO_SparepartSearchResultList(List<LoadingPlanMng_SparepartSearchResult_View> dbItems)
        {
            return Mapper.Map<List<LoadingPlanMng_SparepartSearchResult_View>, List<DTO.LoadingPlanMng.SparepartSearchResult>>(dbItems);
        }

        public DTO.LoadingPlanMng.LoadingPlan DB2DTO_LoadingPlan(LoadingPlanMng_LoadingPlan_View dbItem)
        {
            DTO.LoadingPlanMng.LoadingPlan dtoItem = Mapper.Map<LoadingPlanMng_LoadingPlan_View, DTO.LoadingPlanMng.LoadingPlan>(dbItem);
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);
            return dtoItem;
        }

        public void DTO2DB(DTO.LoadingPlanMng.LoadingPlan dtoItem, ref LoadingPlan dbItem)
        {
            // map parent
            AutoMapper.Mapper.Map<DTO.LoadingPlanMng.LoadingPlan, LoadingPlan>(dtoItem, dbItem);
            dbItem.LoadingDate = dtoItem.LoadingDate.ConvertStringToDateTime();
            dbItem.SentDate = dtoItem.SentDate.ConvertStringToDateTime();
            dbItem.ShipmentDate = dtoItem.ShipmentDate.ConvertStringToDateTime();
            dbItem.ShippedDate = dtoItem.ShippedDate.ConvertStringToDateTime();
            dbItem.UpdatedDate = DateTime.Now;

            // map child - detail
            if (dtoItem.Details != null && dtoItem.Details.Count > 0)
            {
                foreach (LoadingPlanDetail dbDetailToBeDeleted in dbItem.LoadingPlanDetail.ToArray())
                {
                    if (!dtoItem.Details.Select(o => o.LoadingPlanDetailID).Contains(dbDetailToBeDeleted.LoadingPlanDetailID))
                    {
                        dbItem.LoadingPlanDetail.Remove(dbDetailToBeDeleted);
                    }
                }

                // create + update
                foreach (DTO.LoadingPlanMng.LoadingPlanDetail dtoDetail in dtoItem.Details)
                {
                    LoadingPlanDetail dbDetail;
                    if (dtoDetail.LoadingPlanDetailID <= 0)
                    {
                        //create new loading plandetail
                        dbDetail = new LoadingPlanDetail();
                        dbDetail.LoadingPlan = dbItem;
                        dbItem.LoadingPlanDetail.Add(dbDetail);                        
                    }
                    else
                    {
                        //get loadingplandetail to updated
                        dbDetail = dbItem.LoadingPlanDetail.FirstOrDefault(o => o.LoadingPlanDetailID == dtoDetail.LoadingPlanDetailID);                        
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LoadingPlanMng.LoadingPlanDetail, LoadingPlanDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            // map child - sparepart
            if (dtoItem.Spareparts != null && dtoItem.Spareparts.Count > 0)
            {
                // delete case
                foreach (LoadingPlanSparepartDetail dbDetailToBeDeleted in dbItem.LoadingPlanSparepartDetail.ToArray())
                {
                    if (!dtoItem.Spareparts.Select(o => o.LoadingPlanSparepartDetailID).Contains(dbDetailToBeDeleted.LoadingPlanSparepartDetailID))
                    {
                        dbItem.LoadingPlanSparepartDetail.Remove(dbDetailToBeDeleted);
                    }
                }

                // create + update
                foreach (DTO.LoadingPlanMng.LoadingPlanSparepartDetail dtoDetail in dtoItem.Spareparts)
                {
                    LoadingPlanSparepartDetail dbDetail;
                    if (dtoDetail.LoadingPlanSparepartDetailID <= 0)
                    {
                        dbDetail = new LoadingPlanSparepartDetail();
                        dbDetail.LoadingPlan = dbItem;
                        dbItem.LoadingPlanSparepartDetail.Add(dbDetail);                        
                    }
                    else
                    {
                        //get loadingplan sparepart detail to updated
                        dbDetail = dbItem.LoadingPlanSparepartDetail.FirstOrDefault(o => o.LoadingPlanSparepartDetailID == dtoDetail.LoadingPlanSparepartDetailID);                        
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LoadingPlanMng.LoadingPlanSparepartDetail, LoadingPlanSparepartDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            // map child - sample
            if (dtoItem.SampleProducts != null && dtoItem.SampleProducts.Count > 0)
            {
                foreach (LoadingPlanSampleDetail dbDetailToBeDeleted in dbItem.LoadingPlanSampleDetail.ToArray())
                {
                    if (!dtoItem.SampleProducts.Select(o => o.LoadingPlanSampleDetailID).Contains(dbDetailToBeDeleted.LoadingPlanSampleDetailID))
                    {
                        dbItem.LoadingPlanSampleDetail.Remove(dbDetailToBeDeleted);
                    }
                }

                // create + update
                foreach (DTO.LoadingPlanMng.LoadingPlanSampleProductDetail dtoDetail in dtoItem.SampleProducts)
                {
                    LoadingPlanSampleDetail dbDetail;
                    if (dtoDetail.LoadingPlanSampleDetailID <= 0)
                    {
                        //create new loading plandetail
                        dbDetail = new LoadingPlanSampleDetail();
                        dbDetail.LoadingPlan = dbItem;
                        dbItem.LoadingPlanSampleDetail.Add(dbDetail);
                    }
                    else
                    {
                        //get loadingplandetail to updated
                        dbDetail = dbItem.LoadingPlanSampleDetail.FirstOrDefault(o => o.LoadingPlanSampleDetailID == dtoDetail.LoadingPlanSampleDetailID);
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LoadingPlanMng.LoadingPlanSampleProductDetail, LoadingPlanSampleDetail>(dtoDetail, dbDetail);
                    }
                }
            }
        }

        public DTO.LoadingPlanMng.LoadingPlanOverView DB2DTO_LoadingPlanOverView(LoadingPlanMng_LoadingPlan_OverView dbItem)
        {
            DTO.LoadingPlanMng.LoadingPlanOverView dtoItem = Mapper.Map<LoadingPlanMng_LoadingPlan_OverView, DTO.LoadingPlanMng.LoadingPlanOverView>(dbItem);
            return dtoItem;
        }
    }
}
