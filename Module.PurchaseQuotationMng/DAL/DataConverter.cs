using AutoMapper;
using Library;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Module.PurchaseQuotationMng.DAL
{
    internal class DataConverter
    {

        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            #region ** Code developer Luu Nhut **

            AutoMapper.Mapper.CreateMap<PurchaseQuotationMng_PurchaseQuotationSearch_View, DTO.PurchaseQuotationSearchDto>()
                      .IgnoreAllNonExisting()
                      .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                      .ForMember(d => d.ValidTo, o => o.ResolveUsing(s => s.ValidTo.HasValue ? s.ValidTo.Value.ToString("dd/MM/yyyy") : ""))
                      .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));


            AutoMapper.Mapper.CreateMap<PurchaseQuotationMng_PurchaseQuotation_view, DTO.PurchaseQuotationDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.PurchaseQuotationDetailDTOs, o => o.MapFrom(s => s.PurchaseQuotationMng_PurchaseQuotationDetail_View))
                .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.ValidTo, o => o.ResolveUsing(s => s.ValidTo.HasValue ? s.ValidTo.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PurchaseQuotationMng_PurchaseQuotationDetail_View, DTO.PurchaseQuotationDetailDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.PurchaseQuotationDetailDTO, PurchaseQuotationDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.PurchaseQuotationDetailID, o => o.Ignore());

            AutoMapper.Mapper.CreateMap<DTO.PurchaseQuotationDTO, PurchaseQuotation>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.PurchaseQuotationID, o => o.Ignore())
                .ForMember(d => d.PurchaseQuotationUD, o => o.Ignore())
                .ForMember(d => d.ValidFrom, o => o.Ignore())
                .ForMember(d => d.ValidTo, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.ApprovedBy, o => o.Ignore())
                .ForMember(d => d.ApprovedDate, o => o.Ignore())
                .ForMember(d => d.PurchaseQuotationDetail, o => o.Ignore());


            //For support Delivery
            AutoMapper.Mapper.CreateMap<SupportMng_PurchaseDeliveryTerm_View, DTO.SupportDeliveryTermDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //For support PaymentTerm
            AutoMapper.Mapper.CreateMap<SupportMng_PurchasePaymentTerm_View, DTO.SupportPaymentTermDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //For Support FactoryRawMaterial
            AutoMapper.Mapper.CreateMap<PurchaseQoutationMng_GetFactoryRawMaterial_View, DTO.SupportFactoryRawMaterialDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View, DTO.DefaultPricePurchaseQuotationDetailData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PurchaseQuotationMng_DefaultPrice_ProductionItem_View, DTO.DefaultPriceProductionItemData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            #endregion

            #region ** Code developer Truong Son **

            AutoMapper.Mapper.CreateMap<PurchaseQuotationMng_DefaultPrice_View, DTO.PurchaseQuotationDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.Suppliers, o => o.MapFrom(s => s.PurchaseQuotationMng_DefaultPrice_Suppliers_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PurchaseQuotationMng_DefaultPrice_Suppliers_View, DTO.SupplierDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PurchaseQuotationMng_FactoryRawMaterial_View, DTO.SupplierDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            #endregion

            Mapper.CreateMap<PurchaseQuotationMng_ProductionItemDefaultPrice_View, DTO.ProductionItemDefaultPriceDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<ProductionItemGroup, DTO.MaterialGroup>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.MaterialGroupID, o => o.ResolveUsing(s => s.ProductionItemGroupID))
                .ForMember(d => d.MaterialGroupNM, o => o.ResolveUsing(s => s.ProductionItemGroupNM))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<SupportMng_ProductionItem_View, DTO.Material>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.MaterialID, o => o.ResolveUsing(s => s.ProductionItemID))
                .ForMember(d => d.MaterialUD, o => o.ResolveUsing(s => s.ProductionItemUD))
                .ForMember(d => d.MaterialNM, o => o.ResolveUsing(s => s.ProductionItemNM))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<FactoryRawMaterial, DTO.SubSupplier>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.SupplierID, o => o.ResolveUsing(s => s.FactoryRawMaterialID))
                .ForMember(d => d.SupplierUD, o => o.ResolveUsing(s => s.FactoryRawMaterialUD))
                .ForMember(d => d.SupplierNM, o => o.ResolveUsing(s => s.FactoryRawMaterialShortNM))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<PurchasingPriceFactoryOverview_PurchasingPriceFactory_View, DTO.PurchasingPriceFactory>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.ValidTo, o => o.ResolveUsing(s => s.ValidTo.HasValue ? s.ValidTo.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        #region ** Code developer Luu Nhut **

        public List<DTO.PurchaseQuotationSearchDto> BD2DTO_PurchaseQuotationSearch(List<PurchaseQuotationMng_PurchaseQuotationSearch_View> items)
        {
            return AutoMapper.Mapper.Map<List<PurchaseQuotationMng_PurchaseQuotationSearch_View>, List<DTO.PurchaseQuotationSearchDto>>(items);
        }

        public DTO.PurchaseQuotationDTO DB2DTO_PurchaseQuotation(PurchaseQuotationMng_PurchaseQuotation_view dbItem)
        {
            return AutoMapper.Mapper.Map<PurchaseQuotationMng_PurchaseQuotation_view, DTO.PurchaseQuotationDTO>(dbItem);
        }

        public void DTO2DB_PurchaseQuatation(DTO.PurchaseQuotationDTO dtoItem, ref PurchaseQuotation dbItem)
        {
            if (dtoItem.PurchaseQuotationDetailDTOs != null)
            {
                foreach (var item in dbItem.PurchaseQuotationDetail.ToArray())
                {
                    if (!dtoItem.PurchaseQuotationDetailDTOs.Select(s => s.PurchaseQuotationDetailID).Contains(item.PurchaseQuotationDetailID))
                    {
                        dbItem.PurchaseQuotationDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.PurchaseQuotationDetailDTOs)
                {
                    PurchaseQuotationDetail dbDetail = new PurchaseQuotationDetail();

                    if (item.PurchaseQuotationDetailID < 0)
                    {
                        dbItem.PurchaseQuotationDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchaseQuotationDetail.Where(s => s.PurchaseQuotationDetailID == item.PurchaseQuotationDetailID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchaseQuotationDetailDTO, PurchaseQuotationDetail>(item, dbDetail);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.PurchaseQuotationDTO, PurchaseQuotation>(dtoItem, dbItem);
            dbItem.ValidFrom = dtoItem.ValidFrom.ConvertStringToDateTime();
            dbItem.ValidTo = dtoItem.ValidTo.ConvertStringToDateTime();
        }

        //for Suport DeliveryTerm
        public List<DTO.SupportDeliveryTermDTO> DB2DTO_SpDeliveryTerm(List<SupportMng_PurchaseDeliveryTerm_View> Dlist)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PurchaseDeliveryTerm_View>, List<DTO.SupportDeliveryTermDTO>>(Dlist);
        }

        //For support PaymentTerm
        public List<DTO.SupportPaymentTermDTO> DB2DTO_SpPaymentTerm(List<SupportMng_PurchasePaymentTerm_View> Plist)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PurchasePaymentTerm_View>, List<DTO.SupportPaymentTermDTO>>(Plist);
        }

        //For Support FactoryRawMaterial
        public List<DTO.SupportFactoryRawMaterialDTO> DB2DTO_GetFactoryRawMaterial(List<PurchaseQoutationMng_GetFactoryRawMaterial_View> Flist)
        {
            return AutoMapper.Mapper.Map<List<PurchaseQoutationMng_GetFactoryRawMaterial_View>, List<DTO.SupportFactoryRawMaterialDTO>>(Flist);
        }

        #endregion

        #region ** Code developer Truong Son **

        public List<DTO.PurchaseQuotationDTO> DB2DTO_GetProductionItem(List<PurchaseQuotationMng_DefaultPrice_View> items)
        {
            return AutoMapper.Mapper.Map<List<PurchaseQuotationMng_DefaultPrice_View>, List<DTO.PurchaseQuotationDTO>>(items);
        }

        public List<DTO.SupplierDTO> DB2DTO_GetDefaultPrice_Suppliers(List<PurchaseQuotationMng_DefaultPrice_Suppliers_View> items)
        {
            return AutoMapper.Mapper.Map<List<PurchaseQuotationMng_DefaultPrice_Suppliers_View>, List<DTO.SupplierDTO>>(items);
        }

        public List<DTO.SupplierDTO> DB2DTO_GetSuppliers(List<PurchaseQuotationMng_FactoryRawMaterial_View> items)
        {
            return AutoMapper.Mapper.Map<List<PurchaseQuotationMng_FactoryRawMaterial_View>, List<DTO.SupplierDTO>>(items);
        }

        #endregion

        public List<DTO.DefaultPriceProductionItemData> DB2DTO_DefaultProductionItem(List<PurchaseQuotationMng_DefaultPrice_ProductionItem_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseQuotationMng_DefaultPrice_ProductionItem_View>, List<DTO.DefaultPriceProductionItemData>>(dbItem);
        }

        public List<DTO.DefaultPricePurchaseQuotationDetailData> DB2DTO_DefaultPurchasingDetail(List<PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View>, List<DTO.DefaultPricePurchaseQuotationDetailData>>(dbItem);
        }

        public List<DTO.ProductionItemDefaultPriceDTO> DB2DTO_ProductionItemDefaultPrice(List<PurchaseQuotationMng_ProductionItemDefaultPrice_View> dbItem)
        {
            return Mapper.Map<List<PurchaseQuotationMng_ProductionItemDefaultPrice_View>, List<DTO.ProductionItemDefaultPriceDTO>>(dbItem);
        }
    }
}
