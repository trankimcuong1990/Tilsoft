using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.ProductionPriceMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ProductionPriceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ProductionPriceMng_ProductionPriceSearch_View, DTO.ProductionPriceSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    //.ForMember(d => d.ProductionPriceDetailSearchDTOs, o => o.MapFrom(s => s.ProductionPriceMng_ProductionPriceDetailSearch_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionPriceMng_ProductionPriceDetailSearch_View, DTO.ProductionPriceDetailSearchDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionPriceMng_ProductionPrice_View, DTO.ProductionPriceDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LockedDate, o => o.ResolveUsing(s => s.LockedDate.HasValue ? s.LockedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ProductionPriceDetailDTOs, o => o.MapFrom(s => s.ProductionPriceMng_ProductionPriceDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionPriceMng_ProductionPriceDetail_View, DTO.ProductionPriceDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionPriceDetailDTO, ProductionPriceDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ProductionPriceDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductionPriceDTO, ProductionPrice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionPriceID, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                     .ForMember(d => d.LockedBy, o => o.Ignore())
                    .ForMember(d => d.LockedDate, o => o.Ignore())
                    .ForMember(d => d.ProductionPriceDetail, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionPriceMng_AvgPrice_View, DTO.AvgPriceDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AvgPriceDTO, DTO.ProductionPriceDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionPriceMng_ReceiptByProductionItem_View, DTO.ReceiptByProductionItem>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ProductionPriceSearchDTO> DB2DTO_ProductionPriceSearch(List<ProductionPriceMng_ProductionPriceSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionPriceMng_ProductionPriceSearch_View>, List<DTO.ProductionPriceSearchDTO>>(dbItems);
        }

        public DTO.ProductionPriceDTO DB2DTO_ProductionPrice(ProductionPriceMng_ProductionPrice_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionPriceMng_ProductionPrice_View, DTO.ProductionPriceDTO>(dbItem);
        }

        public void DTO2DB_ProductionPrice(DTO.ProductionPriceDTO dtoItem, ref ProductionPrice dbItem, int? userId, int? companyID)
        {
            if (dtoItem.ProductionPriceDetailDTOs != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.ProductionPriceDetail.ToArray())
                {
                    if (!dtoItem.ProductionPriceDetailDTOs.Select(s => s.ProductionPriceDetailID).Contains(item.ProductionPriceDetailID))
                    {
                        dbItem.ProductionPriceDetail.Remove(item);
                    }                    
                }
                //read from dto to db
                ProductionPriceDetail dbPurchaseDetail;
                foreach (var item in dtoItem.ProductionPriceDetailDTOs)
                {
                    if (item.ProductionPriceDetailID > 0)
                    {
                        dbPurchaseDetail = dbItem.ProductionPriceDetail.Where(o => o.ProductionPriceDetailID == item.ProductionPriceDetailID).FirstOrDefault();                        
                    }
                    else
                    {
                        dbPurchaseDetail = new ProductionPriceDetail();
                        dbItem.ProductionPriceDetail.Add(dbPurchaseDetail);                        
                    }
                    //read purchase request detail dto to db
                    if (dbPurchaseDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductionPriceDetailDTO, ProductionPriceDetail>(item, dbPurchaseDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.ProductionPriceDTO, ProductionPrice>(dtoItem, dbItem);
            dbItem.CompanyID = companyID;
            dbItem.UpdatedBy = userId;
            dbItem.UpdatedDate = DateTime.Now;
            if (!dtoItem.ProductionPriceID.HasValue)
            {
                dbItem.CreatedBy = userId;
                dbItem.CreatedDate = DateTime.Now;
            }
        }
    }
}
