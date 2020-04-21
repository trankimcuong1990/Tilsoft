using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.WarehouseTransportMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WarehouseTransportMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                
                AutoMapper.Mapper.CreateMap<WarehouseTransportMng_WarehouseTransport_SearchView, DTO.WarehouseTransportMng.WarehouseTransportSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TransportDate, o => o.ResolveUsing(s => s.TransportDate.HasValue ? s.TransportDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransportMng_WarehouseTransport_View, DTO.WarehouseTransportMng.WarehouseTransport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TransportDate, o => o.ResolveUsing(s => s.TransportDate.HasValue ? s.TransportDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.ResolveUsing(s => s.ConcurrencyFlag != null ? Convert.ToBase64String(s.ConcurrencyFlag) : ""))
                    .ForMember(d => d.WarehouseTransportProductDetails, o => o.MapFrom(s => s.WarehouseTransportMng_WarehouseTransportProductDetail_View))
                    .ForMember(d => d.WarehouseTransportSparepartDetails, o => o.MapFrom(s => s.WarehouseTransportMng_WarehouseTransportSparepartDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                AutoMapper.Mapper.CreateMap<WarehouseTransportMng_WarehouseTransportProductDetail_View, DTO.WarehouseTransportMng.WarehouseTransportProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransportMng_WarehouseTransportSparepartDetail_View, DTO.WarehouseTransportMng.WarehouseTransportSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WarehouseTransportMng.WarehouseTransport, WarehouseTransport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseTransportProductDetail, o => o.Ignore())
                    .ForMember(d => d.WarehouseTransportProductDetail, o => o.Ignore())
                    .ForMember(d => d.WarehouseTransportSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.TransportDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.WarehouseTransportMng.WarehouseTransportProductDetail, WarehouseTransportProductDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.WarehouseTransportProductDetailID, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<DTO.WarehouseTransportMng.WarehouseTransportSparepartDetail, WarehouseTransportSparepartDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.WarehouseTransportSparepartDetailID, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<WarehouseTransportMng_PhysicalStock_View, DTO.WarehouseTransportMng.PhysicalStock>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransportMng_PhysicalStockByWarehouseArea_View, DTO.WarehouseTransportMng.PhysicalStockByWarehouseArea>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public List<DTO.WarehouseTransportMng.WarehouseTransportSearch> DB2DTO_WarehouseTransportSearch(List<WarehouseTransportMng_WarehouseTransport_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransportMng_WarehouseTransport_SearchView>, List<DTO.WarehouseTransportMng.WarehouseTransportSearch>>(dbItems);
        }
        public DTO.WarehouseTransportMng.WarehouseTransport DB2DTO_WarehouseTransport(WarehouseTransportMng_WarehouseTransport_View dbItem)
        {
            DTO.WarehouseTransportMng.WarehouseTransport dtoItem = AutoMapper.Mapper.Map<WarehouseTransportMng_WarehouseTransport_View, DTO.WarehouseTransportMng.WarehouseTransport>(dbItem);
            return dtoItem;
        }
        public void DTO2DB_WarehouseTransport(DTO.WarehouseTransportMng.WarehouseTransport dtoItem, ref WarehouseTransport dbItem)
        {
            List<WarehouseTransportProductDetail> product_tobedeleted = new List<WarehouseTransportProductDetail>();
            if (dtoItem.WarehouseTransportProductDetails != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.WarehouseTransportProductDetail.Where(o => !dtoItem.WarehouseTransportProductDetails.Select(s => s.WarehouseTransportProductDetailID).Contains(o.WarehouseTransportProductDetailID)))
                {
                    product_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in product_tobedeleted)
                {
                    dbItem.WarehouseTransportProductDetail.Remove(dbDetail);
                }
                //MAP
                foreach (var dtoDetail in dtoItem.WarehouseTransportProductDetails)
                {
                    if (!dtoDetail.FromWarehouseAreaID.HasValue || !dtoDetail.ToWarehouseAreaID.HasValue || dtoDetail.FromWarehouseAreaID.Value == 0 || dtoDetail.ToWarehouseAreaID.Value == 0)
                    {
                        throw new Exception("You must fill-in 'From area' and 'To area' for all product");
                    }
                    
                    WarehouseTransportProductDetail dbDetail;
                    if (dtoDetail.WarehouseTransportProductDetailID < 0)
                    {
                        dbDetail = new WarehouseTransportProductDetail();
                        dbItem.WarehouseTransportProductDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.WarehouseTransportProductDetail.FirstOrDefault(o => o.WarehouseTransportProductDetailID == dtoDetail.WarehouseTransportProductDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehouseTransportMng.WarehouseTransportProductDetail, WarehouseTransportProductDetail>(dtoDetail, dbDetail);
                    }
                }
            }
            //Purchasing Invoice Sparepart Detail
            List<WarehouseTransportSparepartDetail> sparepart_tobedeleted = new List<WarehouseTransportSparepartDetail>();
            if (dtoItem.WarehouseTransportSparepartDetails != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.WarehouseTransportSparepartDetail.Where(o => !dtoItem.WarehouseTransportSparepartDetails.Select(s => s.WarehouseTransportSparepartDetailID).Contains(o.WarehouseTransportSparepartDetailID)))
                {
                    sparepart_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in sparepart_tobedeleted)
                {
                    dbItem.WarehouseTransportSparepartDetail.Remove(dbDetail);
                }
                //MAP
                foreach (var dtoDetail in dtoItem.WarehouseTransportSparepartDetails)
                {
                    if (!dtoDetail.FromWarehouseAreaID.HasValue || !dtoDetail.ToWarehouseAreaID.HasValue || dtoDetail.FromWarehouseAreaID.Value == 0 || dtoDetail.ToWarehouseAreaID.Value == 0)
                    {
                        throw new Exception("You must fill-in 'From area' and 'To area' for all sparepart");
                    }
                    
                    WarehouseTransportSparepartDetail dbDetail;
                    if (dtoDetail.WarehouseTransportSparepartDetailID < 0)
                    {
                        dbDetail = new WarehouseTransportSparepartDetail();
                        dbItem.WarehouseTransportSparepartDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.WarehouseTransportSparepartDetail.FirstOrDefault(o => o.WarehouseTransportSparepartDetailID == dtoDetail.WarehouseTransportSparepartDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehouseTransportMng.WarehouseTransportSparepartDetail, WarehouseTransportSparepartDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            //Purchasing Invoice
            AutoMapper.Mapper.Map<DTO.WarehouseTransportMng.WarehouseTransport, WarehouseTransport>(dtoItem, dbItem);
            if (dtoItem.WarehouseTransportID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
            dbItem.TransportDate = dtoItem.TransportDate.ConvertStringToDateTime();
        }

        public List<DTO.WarehouseTransportMng.PhysicalStock> DB2DTO_PhysicalStock(List<WarehouseTransportMng_PhysicalStock_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransportMng_PhysicalStock_View>, List<DTO.WarehouseTransportMng.PhysicalStock>>(dbItems);
        }

        public List<DTO.WarehouseTransportMng.PhysicalStockByWarehouseArea> DB2DTO_PhysicalStockByWarehouseArea(List<WarehouseTransportMng_PhysicalStockByWarehouseArea_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransportMng_PhysicalStockByWarehouseArea_View>, List<DTO.WarehouseTransportMng.PhysicalStockByWarehouseArea>>(dbItems);
        }
        
    }
}
