using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.FactoryLoadingPlanMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "FactoryLoadingPlanMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryLoadingPlanMng_LoadingPlanSearchResult_View, DTO.LoadingPlanSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryLoadingPlanMng_BookingSearchResult_View, DTO.BookingSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CutOffDate, o => o.ResolveUsing(s => s.CutOffDate.HasValue ? s.CutOffDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryLoadingPlanMng_LoadingPlan_View, DTO.LoadingPlan>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipmentDate, o => o.ResolveUsing(s => s.ShipmentDate.HasValue ? s.ShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.CutOffDate, o => o.ResolveUsing(s => s.CutOffDate.HasValue ? s.CutOffDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.LoadingPlanDetails, o => o.MapFrom(s => s.FactoryLoadingPlanMng_LoadingPlanDetail_View))
                    .ForMember(d => d.LoadingPlanSparepartDetails, o => o.MapFrom(s => s.FactoryLoadingPlanMng_LoadingPlanSparepartDetail_View))
                    .ForMember(d => d.ProductPicture1_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ProductPicture1_DisplayUrl) ? s.ProductPicture1_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ProductPicture1_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ProductPicture1_OriginalUrl) ? s.ProductPicture1_OriginalUrl : "no-image.jpg")))
                    .ForMember(d => d.ProductPicture2_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ProductPicture2_DisplayUrl) ? s.ProductPicture2_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ProductPicture2_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ProductPicture2_OriginalUrl) ? s.ProductPicture2_OriginalUrl : "no-image.jpg")))
                    .ForMember(d => d.ContainerPicture1_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ContainerPicture1_DisplayUrl) ? s.ContainerPicture1_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ContainerPicture1_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture1_OriginalUrl) ? s.ContainerPicture1_OriginalUrl : "no-image.jpg")))
                    .ForMember(d => d.ContainerPicture2_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ContainerPicture2_DisplayUrl) ? s.ContainerPicture2_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ContainerPicture2_OriginalUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ContainerPicture2_OriginalUrl) ? s.ContainerPicture2_OriginalUrl : "no-image.jpg")))
                    .ForMember(d => d.ParentShipmentDate, o => o.ResolveUsing(s => s.ParentShipmentDate.HasValue ? s.ParentShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ParentLoadingDate, o => o.ResolveUsing(s => s.ParentLoadingDate.HasValue ? s.ParentLoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryLoadingPlanMng_LoadingPlanDetail_View, DTO.LoadingPlanDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryLoadingPlanMng_LoadingPlanSparepartDetail_View, DTO.LoadingPlanSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryLoadingPlanMng_FactoryOrderDetailSearchResult_View, DTO.FactoryOrderDetailSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryLoadingPlanMng_FactoryOrderSparepartDetailSearchResult_View, DTO.FactoryOrderSparepartDetailSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.LoadingPlan, LoadingPlan>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LoadingPlanID, o => o.Ignore())
                    .ForMember(d => d.LoadingPlanUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    //.ForMember(d => d.ParentID, o => o.Ignore())
                    .ForMember(d => d.ShipmentDate, o => o.Ignore())
                    .ForMember(d => d.LoadingDate, o => o.Ignore())
                    .ForMember(d => d.ProductPicture1, o => o.Ignore())
                    .ForMember(d => d.ProductPicture2, o => o.Ignore())
                    .ForMember(d => d.ContainerPicture1, o => o.Ignore())
                    .ForMember(d => d.ContainerPicture2, o => o.Ignore())
                    .ForMember(d => d.LoadingPlanDetail, o => o.Ignore())
                    .ForMember(d => d.LoadingPlanSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LoadingPlanDetail, LoadingPlanDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LoadingPlanDetailID, o => o.Ignore())
                    .ForMember(d => d.LoadingPlanID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LoadingPlanSparepartDetail, LoadingPlanSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LoadingPlanSparepartDetailID, o => o.Ignore())
                    .ForMember(d => d.LoadingPlanID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.LoadingPlanSearchResult> DB2DTO_LoadingPlanSearchResultList(List<FactoryLoadingPlanMng_LoadingPlanSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryLoadingPlanMng_LoadingPlanSearchResult_View>, List<DTO.LoadingPlanSearchResult>>(dbItems);
        }

        public List<DTO.BookingSearchResult> DB2DTO_BookingSearchResultList(List<FactoryLoadingPlanMng_BookingSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryLoadingPlanMng_BookingSearchResult_View>, List<DTO.BookingSearchResult>>(dbItems);
        }

        public DTO.BookingSearchResult DB2DTO_BookingSearchResult(FactoryLoadingPlanMng_BookingSearchResult_View dbItems)
        {
            return AutoMapper.Mapper.Map<FactoryLoadingPlanMng_BookingSearchResult_View, DTO.BookingSearchResult>(dbItems);
        }

        public DTO.LoadingPlan DB2DTO_LoadingPlan(FactoryLoadingPlanMng_LoadingPlan_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryLoadingPlanMng_LoadingPlan_View, DTO.LoadingPlan>(dbItem);
        }

        public List<DTO.FactoryOrderDetailSearchResult> DB2DTO_FactoryOrderDetailSearchResultList(List<FactoryLoadingPlanMng_FactoryOrderDetailSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryLoadingPlanMng_FactoryOrderDetailSearchResult_View>, List<DTO.FactoryOrderDetailSearchResult>>(dbItems);
        }

        public List<DTO.FactoryOrderSparepartDetailSearchResult> DB2DTO_FactoryOrderSparepartDetailSearchResultList(List<FactoryLoadingPlanMng_FactoryOrderSparepartDetailSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryLoadingPlanMng_FactoryOrderSparepartDetailSearchResult_View>, List<DTO.FactoryOrderSparepartDetailSearchResult>>(dbItems);
        }

        public void DTO2DB(DTO.LoadingPlan dtoItem, ref LoadingPlan dbItem, string _tempFolder, bool mapDetail)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.LoadingPlan, LoadingPlan>(dtoItem, dbItem);

            // insert image
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            if (dtoItem.ProductPicture1_HasChange)
            {
                dbItem.ProductPicture1 = fwFactory.CreateFilePointer(_tempFolder, dtoItem.ProductPicture1_NewFile, dtoItem.ProductPicture1);
            }
            if (dtoItem.ProductPicture2_HasChange)
            {
                dbItem.ProductPicture2 = fwFactory.CreateFilePointer(_tempFolder, dtoItem.ProductPicture2_NewFile, dtoItem.ProductPicture2);
            }
            if (dtoItem.ContainerPicture1_HasChange)
            {
                dbItem.ContainerPicture1 = fwFactory.CreateFilePointer(_tempFolder, dtoItem.ContainerPicture1_NewFile, dtoItem.ContainerPicture1);
            }
            if (dtoItem.ContainerPicture2_HasChange)
            {
                dbItem.ContainerPicture2 = fwFactory.CreateFilePointer(_tempFolder, dtoItem.ContainerPicture2_NewFile, dtoItem.ContainerPicture2);
            }
            if (!string.IsNullOrEmpty(dtoItem.ShipmentDate))
            {
                if (DateTime.TryParse(dtoItem.ShipmentDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.ShipmentDate = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.LoadingDate))
            {
                if (DateTime.TryParse(dtoItem.LoadingDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.LoadingDate = tmpDate;
                }
            }

            if (mapDetail)
            {
                // map detail
                if (dtoItem.LoadingPlanDetails != null)
                {
                    // check for child rows deleted
                    foreach (LoadingPlanDetail dbDetail in dbItem.LoadingPlanDetail.ToArray())
                    {
                        if (!dtoItem.LoadingPlanDetails.Select(o => o.LoadingPlanDetailID).Contains(dbDetail.LoadingPlanDetailID))
                        {
                            dbItem.LoadingPlanDetail.Remove(dbDetail);
                        }
                    }

                    // map child rows
                    foreach (DTO.LoadingPlanDetail dtoDetail in dtoItem.LoadingPlanDetails)
                    {
                        LoadingPlanDetail dbDetail;
                        if (dtoDetail.LoadingPlanDetailID <= 0)
                        {
                            dbDetail = new LoadingPlanDetail();
                            dbItem.LoadingPlanDetail.Add(dbDetail);
                        }
                        else
                        {
                            dbDetail = dbItem.LoadingPlanDetail.FirstOrDefault(o => o.LoadingPlanDetailID == dtoDetail.LoadingPlanDetailID);
                        }

                        if (dbDetail != null)
                        {
                            AutoMapper.Mapper.Map<DTO.LoadingPlanDetail, LoadingPlanDetail>(dtoDetail, dbDetail);

                        }
                    }
                }

                // map sparepart detail
                if (dtoItem.LoadingPlanSparepartDetails != null)
                {
                    // check for child rows deleted
                    foreach (LoadingPlanSparepartDetail dbSparepartDetail in dbItem.LoadingPlanSparepartDetail.ToArray())
                    {
                        if (!dtoItem.LoadingPlanSparepartDetails.Select(o => o.LoadingPlanSparepartDetailID).Contains(dbSparepartDetail.LoadingPlanSparepartDetailID))
                        {
                            dbItem.LoadingPlanSparepartDetail.Remove(dbSparepartDetail);
                        }
                    }

                    // map child rows
                    foreach (DTO.LoadingPlanSparepartDetail dtoSparepartDetail in dtoItem.LoadingPlanSparepartDetails)
                    {
                        LoadingPlanSparepartDetail dbSparepartDetail;
                        if (dtoSparepartDetail.LoadingPlanSparepartDetailID <= 0)
                        {
                            dbSparepartDetail = new LoadingPlanSparepartDetail();
                            dbItem.LoadingPlanSparepartDetail.Add(dbSparepartDetail);
                        }
                        else
                        {
                            dbSparepartDetail = dbItem.LoadingPlanSparepartDetail.FirstOrDefault(o => o.LoadingPlanSparepartDetailID == dtoSparepartDetail.LoadingPlanSparepartDetailID);
                        }

                        if (dbSparepartDetail != null)
                        {
                            AutoMapper.Mapper.Map<DTO.LoadingPlanSparepartDetail, LoadingPlanSparepartDetail>(dtoSparepartDetail, dbSparepartDetail);

                        }
                    }
                } 
            }            
        }
    }
}
