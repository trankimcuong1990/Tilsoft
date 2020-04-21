using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.OfferSeasonMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DB 2 DTO
                //
                AutoMapper.Mapper.CreateMap<OfferSeasonMng_OfferSeasonSearch_View, DTO.OfferSeasonSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_GetItemPendingPrice_Result, DTO.FactoryPendingPriceDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_OfferSeason_View, DTO.OfferSeasonDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_OfferSeasonDetail_View, DTO.OfferSeasonDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.SetItemStatusDate, o => o.ResolveUsing(s => s.SetItemStatusDate.HasValue ? s.SetItemStatusDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.ResolveUsing(s => s.PlaningPurchasingPriceSelectedDate.HasValue ? s.PlaningPurchasingPriceSelectedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.EstimatedPurchasingPriceUpdatedDate, o => o.ResolveUsing(s => s.EstimatedPurchasingPriceUpdatedDate.HasValue ? s.EstimatedPurchasingPriceUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.UnApprovedDate, o => o.ResolveUsing(s => s.UnApprovedDate.HasValue ? s.UnApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.NeedFactoryQuotationDate, o => o.ResolveUsing(s => s.NeedFactoryQuotationDate.HasValue ? s.NeedFactoryQuotationDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ClientSelectedDate, o => o.ResolveUsing(s => s.ClientSelectedDate.HasValue ? s.ClientSelectedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + s.ProductFileLocation))
                   .ForMember(d => d.ProductThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.ProductThumbnailLocation))
                   .ForMember(d => d.PlaningPurchasingPriceFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + s.PlaningPurchasingPriceFileLocation))
                   .ForMember(d => d.OfferSeasonDetailPriceOptionDTOs, o => o.MapFrom(s => s.OfferSeasonMng_OfferSeasonDetailPriceOption_View))                   
                   .ForMember(d => d.OfferSeasonDetailRemarkDTOs, o => o.MapFrom(s => s.OfferSeasonMng_OfferSeasonDetailRemark_View.OrderByDescending(q => q.RemarkDate)))
                   .ForMember(d => d.OfferSeasonDetailPriceHistoryDTOs, o => o.MapFrom(s => s.OfferSeasonMng_OfferSeasonDetailPriceHistory_View.OrderByDescending(q => q.UpdatedDate)))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_OfferSeasonDetailPriceOption_View, DTO.OfferSeasonDetailPriceOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_OfferSeasonDetailRemark_View, DTO.OfferSeasonDetailRemarkDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.RemarkDate, o => o.ResolveUsing(s => s.RemarkDate.HasValue ? s.RemarkDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_OfferSeasonDetailPriceHistory_View, DTO.OfferSeasonDetailPriceHistoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // 
                // MAPPING DTO 2 DB
                //
                AutoMapper.Mapper.CreateMap<DTO.OfferSeasonDetailPriceOptionDTO, OfferSeasonDetailPriceOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferSeasonDetailPriceOptionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.OfferSeasonDetailRemarkDTO, OfferSeasonDetailRemark>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.RemarkDate, o => o.Ignore())
                    .ForMember(d => d.OfferSeasonDetailRemarkID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.OfferSeasonDetailDTO, OfferSeasonDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SetItemStatusDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.Ignore())
                    .ForMember(d => d.EstimatedPurchasingPriceUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.UnApprovedDate, o => o.Ignore())
                    .ForMember(d => d.NeedFactoryQuotationDate, o => o.Ignore())                    
                    .ForMember(d => d.ClientSelectedDate, o => o.Ignore())
                    .ForMember(d => d.OfferSeasonDetailPriceOption, o => o.Ignore())
                    .ForMember(d => d.OfferSeasonDetailID, o => o.Ignore())

                    .ForMember(d => d.ModelID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialColorID, o => o.Ignore())
                    .ForMember(d => d.MaterialID, o => o.Ignore())
                    .ForMember(d => d.MaterialTypeID, o => o.Ignore())
                    .ForMember(d => d.MaterialColorID, o => o.Ignore())
                    .ForMember(d => d.SubMaterialID, o => o.Ignore())
                    .ForMember(d => d.SubMaterialColorID, o => o.Ignore())
                    .ForMember(d => d.SeatCushionID, o => o.Ignore())
                    .ForMember(d => d.BackCushionID, o => o.Ignore())
                    .ForMember(d => d.CushionColorID, o => o.Ignore())
                    .ForMember(d => d.FSCTypeID, o => o.Ignore())
                    .ForMember(d => d.FSCPercentID, o => o.Ignore())
                    .ForMember(d => d.ArticleCode, o => o.Ignore())
                    .ForMember(d => d.Description, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.OfferSeasonDTO, OfferSeason>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.OfferSeasonDetail, o => o.Ignore())
                    .ForMember(d => d.OfferSeasonID, o => o.Ignore());

                //
                //custom mapping
                //
                AutoMapper.Mapper.CreateMap<SupportMng_OfferSeasonType_View, DTO.OfferSeasonTypeDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_Model_View, DTO.ModelDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_Product_View, DTO.ProductDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_Sparepart_View, DTO.SparepartDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_OfferItemProperies_View, DTO.OfferItemProperiesDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.MaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.MaterialThumbnailLocation))
                   .ForMember(d => d.FrameMaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.FrameMaterialThumbnailLocation))
                   .ForMember(d => d.SubMaterialColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SubMaterialColorThumbnailLocation))
                   .ForMember(d => d.CushionColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.CushionColorThumbnailLocation))
                   .ForMember(d => d.BackCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.BackCushionThumbnailLocation))
                   .ForMember(d => d.SeatCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SeatCushionThumbnailLocation))
                   .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + s.ProductFileLocation))
                   .ForMember(d => d.ProductThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.ProductThumbnailLocation))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_GetOfferItemDefaultProperties_Result, DTO.OfferItemDefaultPropertiesDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.MaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.MaterialThumbnailLocation))
                   .ForMember(d => d.FrameMaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.FrameMaterialThumbnailLocation))
                   .ForMember(d => d.SubMaterialColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SubMaterialColorThumbnailLocation))
                   .ForMember(d => d.CushionColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.CushionColorThumbnailLocation))
                   .ForMember(d => d.BackCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.BackCushionThumbnailLocation))
                   .ForMember(d => d.SeatCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SeatCushionThumbnailLocation))
                   .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + s.ProductFileLocation))
                   .ForMember(d => d.ProductThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.ProductThumbnailLocation))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_ClientEstimatedAdditionalCost_View, DTO.ClientEstimatedAdditionalCostDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_Client_View, DTO.ClientDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ClientEstimatedAdditionalCostDTOs, o => o.MapFrom(s => s.OfferSeasonMng_ClientEstimatedAdditionalCost_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_ModelSparepart_View, DTO.ModelSparepartDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_PlanningPurchasingPrice_View, DTO.PlanningPurchasingPriceDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_PlaningPurchasingPriceSource_View, DTO.PlaningPurchasingPriceSourceDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_Factory_View, DTO.FactoryDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_OfferSeasonItemStatus_View, DTO.OfferSeasonItemStatusDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductElement_View, DTO.ProductElementDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_GetSalePrice_Result, DTO.SalePriceTable>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_GetSalePriceLastSeason_Result, DTO.SalePriceTableLastSeason>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_SaleOrderDetail_View, DTO.SaleOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_OfferLine_View, DTO.OfferLineDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailDTOs, o => o.MapFrom(s => s.OfferSeasonMng_SaleOrderDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_GetImageGallery_Result, DTO.ImageGalleryDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageGalleryFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + s.ImageGalleryFileLocation))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_CreateOfferSeasonSample_Result, DTO.SampleOfferSeasonDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_GetRelatedFactoryOrderDetail_Result, DTO.RelatedFactoryOrderDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_GetCurrentSupplier_Result, DTO.PurchasingPriceLastYearDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_function_GetMasterSetting_Result, DTO.MasterSettingDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonMng_ActiveSales_View, DTO.AccManagerDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.OfferSeasonSearchDTO> DB2DTO_Search(List<OfferSeasonMng_OfferSeasonSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonMng_OfferSeasonSearch_View>, List<DTO.OfferSeasonSearchDTO>>(dbItems);
        }

        public DTO.OfferSeasonDTO DB2DTO_OfferSeason(OfferSeasonMng_OfferSeason_View dbItem)
        {
            return AutoMapper.Mapper.Map<OfferSeasonMng_OfferSeason_View, DTO.OfferSeasonDTO>(dbItem);
        }

        public void DTO2DB_OfferSeason(int userId, DTO.OfferSeasonDTO dtoItem, ref OfferSeason dbItem)
        {
            AutoMapper.Mapper.Map<DTO.OfferSeasonDTO, OfferSeason>(dtoItem, dbItem);
            if (dtoItem.OfferSeasonID <= 0)
            {
                dbItem.CreatedBy = userId;
                dbItem.CreatedDate = DateTime.Now;
            }
        }

        public List<DTO.OfferSeasonDetailDTO> DB2DTO_OfferSeasonDetail(List<OfferSeasonMng_OfferSeasonDetail_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonMng_OfferSeasonDetail_View>, List<DTO.OfferSeasonDetailDTO>>(dbItem);
        }

        public void DTO2DB_OfferSeasonDetail(int userId, int? offerSeasonTypeID, int offerSeasonDetailID, DTO.OfferSeasonDetailDTO dtoOfferSeasonDetail, bool isAllowEditProperties, ref OfferSeasonDetail dbItem)
        {
            //
            //some logic on dto item
            //
            if (offerSeasonDetailID <= 0) {
                if (offerSeasonTypeID == 1 || offerSeasonTypeID == 4 || offerSeasonTypeID == 5) //warehouse and sparepart
                {
                    dtoOfferSeasonDetail.ItemStatus = 5; //auto approve item
                }                
            }

            //auto change to status 2: Need Make Quotation if status is in 1: Pendding
            if (dtoOfferSeasonDetail.ItemStatus == 1)
            {
                dtoOfferSeasonDetail.ItemStatus = 2;
            }

            //
            //begin convert dto to db
            //

            //read from dto to db OfferSeasonDetailPriceOption
            OfferSeasonDetailPriceOption dbOfferSeasonDetailPriceOption;
            foreach (var item in dtoOfferSeasonDetail.OfferSeasonDetailPriceOptionDTOs)
            {
                if (item.OfferSeasonDetailPriceOptionID > 0)
                {
                    dbOfferSeasonDetailPriceOption = dbItem.OfferSeasonDetailPriceOption.Where(o => o.OfferSeasonDetailPriceOptionID == item.OfferSeasonDetailPriceOptionID).FirstOrDefault();
                }
                else
                {
                    dbOfferSeasonDetailPriceOption = new OfferSeasonDetailPriceOption();
                    dbItem.OfferSeasonDetailPriceOption.Add(dbOfferSeasonDetailPriceOption);
                }
                if (dbOfferSeasonDetailPriceOption != null)
                {
                    AutoMapper.Mapper.Map<DTO.OfferSeasonDetailPriceOptionDTO, OfferSeasonDetailPriceOption>(item, dbOfferSeasonDetailPriceOption);
                }
            }

            //read from dto to db OfferSeasonDetailRemark
            OfferSeasonDetailRemark dbOfferSeasonDetailRemark;
            foreach (var item in dtoOfferSeasonDetail.OfferSeasonDetailRemarkDTOs.Where(o => o.OfferSeasonDetailRemarkID <= 0))
            {
                dbOfferSeasonDetailRemark = new OfferSeasonDetailRemark();
                dbItem.OfferSeasonDetailRemark.Add(dbOfferSeasonDetailRemark);
                AutoMapper.Mapper.Map<DTO.OfferSeasonDetailRemarkDTO, OfferSeasonDetailRemark>(item, dbOfferSeasonDetailRemark);
                dbOfferSeasonDetailRemark.RemarkBy = userId;
                dbOfferSeasonDetailRemark.RemarkDate = DateTime.Now;
            }

            //map OfferSeasonDetail
            AutoMapper.Mapper.Map<DTO.OfferSeasonDetailDTO, OfferSeasonDetail>(dtoOfferSeasonDetail, dbItem);

            if (isAllowEditProperties)
            {
                dbItem.ModelID = dtoOfferSeasonDetail.ModelID;
                dbItem.FrameMaterialID = dtoOfferSeasonDetail.FrameMaterialID;
                dbItem.FrameMaterialColorID = dtoOfferSeasonDetail.FrameMaterialColorID;
                dbItem.MaterialID = dtoOfferSeasonDetail.MaterialID;
                dbItem.MaterialTypeID = dtoOfferSeasonDetail.MaterialTypeID;
                dbItem.MaterialColorID = dtoOfferSeasonDetail.MaterialColorID;
                dbItem.SubMaterialID = dtoOfferSeasonDetail.SubMaterialID;
                dbItem.SubMaterialColorID = dtoOfferSeasonDetail.SubMaterialColorID;
                dbItem.SeatCushionID = dtoOfferSeasonDetail.SeatCushionID;
                dbItem.BackCushionID = dtoOfferSeasonDetail.BackCushionID;
                dbItem.CushionColorID = dtoOfferSeasonDetail.CushionColorID;
                dbItem.FSCTypeID = dtoOfferSeasonDetail.FSCTypeID;
                dbItem.FSCPercentID = dtoOfferSeasonDetail.FSCPercentID;
                dbItem.ArticleCode = dtoOfferSeasonDetail.ArticleCode;
                dbItem.Description = dtoOfferSeasonDetail.Description;
            }

            //set tracking info
            if (dbItem.OfferSeasonDetailID > 0)
            {
                dbItem.UpdatedBy = userId;
                dbItem.UpdatedDate = DateTime.Now;
            }
            else
            {
                dbItem.CreatedBy = userId;
                dbItem.CreatedDate = DateTime.Now;
            }
            if (dbItem.ItemStatus == 5) // item is apprved
            {
                if(dtoOfferSeasonDetail.MarkAsApproved.HasValue && dtoOfferSeasonDetail.MarkAsApproved.Value)
                {
                    dbItem.SetItemStatusBy = userId;
                    dbItem.SetItemStatusDate = DateTime.Now;
                }
            }

            if (dtoOfferSeasonDetail.PlaningPurchasingPriceSelectedDate == "just now")
            {
                dbItem.PlaningPurchasingPriceSelectedBy = userId;
                dbItem.PlaningPurchasingPriceSelectedDate = DateTime.Now;
            }

            if (dtoOfferSeasonDetail.IsManuallyKeyIn.HasValue && dtoOfferSeasonDetail.IsManuallyKeyIn.Value)
            {
                dbItem.EstimatedPurchasingPriceUpdatedByID = userId;
                dbItem.EstimatedPurchasingPriceUpdatedDate = DateTime.Now;
            }

            if (dtoOfferSeasonDetail.MarkAsUnApproved.HasValue && dtoOfferSeasonDetail.MarkAsUnApproved.Value)
            {
                dbItem.UnApprovedBy = userId;
                dbItem.UnApprovedDate = DateTime.Now;
            }

            if (dtoOfferSeasonDetail.MarkAsNeedFactoryQuotation.HasValue)
            {
                dbItem.NeedFactoryQuotationBy = userId;
                dbItem.NeedFactoryQuotationDate = DateTime.Now;
            }

            if (dtoOfferSeasonDetail.PlaningPurchasingPriceFileHasChange.HasValue && dtoOfferSeasonDetail.PlaningPurchasingPriceFileHasChange.Value)
            {
                string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                dbItem.PlaningPurchasingPriceFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(tempFolder, dtoOfferSeasonDetail.PlaningPurchasingPriceFileNewFile, dtoOfferSeasonDetail.PlaningPurchasingPriceFile, dtoOfferSeasonDetail.PlaningPurchasingPriceFileFriendlyName);
            }

            if (dtoOfferSeasonDetail.MarkAsClientSelected.HasValue)
            {
                dbItem.ClientSelectedBy = userId;
                dbItem.ClientSelectedDate = DateTime.Now;
            }
        }

        //custom mapping
        public List<DTO.ModelDTO> DB2DTO_SearchModel(List<OfferSeasonMng_Model_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonMng_Model_View>, List<DTO.ModelDTO>>(dbItems);
        }

        public List<DTO.ProductDTO> DB2DTO_SearchProduct(List<OfferSeasonMng_Product_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonMng_Product_View>, List<DTO.ProductDTO>>(dbItems);
        }

        public List<DTO.SparepartDTO> DB2DTO_SearchSparepart(List<OfferSeasonMng_Sparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonMng_Sparepart_View>, List<DTO.SparepartDTO>>(dbItems);
        }

    }
}
