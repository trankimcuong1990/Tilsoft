using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.WarehouseImportMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WarehouseImportMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<WarehouseImportMng_WarehouseImportSearchResult_View, DTO.WarehouseImportMng.WarehouseImportSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImportedDate, o => o.ResolveUsing(s => s.ImportedDate.HasValue ? s.ImportedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<WarehouseImportMng_OnSeaProduct_View, DTO.WarehouseImportMng.OnSeaProduct>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_OnSeaSparepart_View, DTO.WarehouseImportMng.OnSeaSparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<WarehouseImportMng_WarehouseImportProductDetail_View, DTO.WarehouseImportMng.WarehouseImportProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseImportAreaDetails, o => o.MapFrom(s => s.WarehouseImportMng_WarehouseImportAreaDetail_View))
                    .ForMember(d => d.WarehouseImportColliDetails, o => o.MapFrom(s => s.WarehouseImportMng_WarehouseImportColliDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_WarehouseImportSparepartDetail_View, DTO.WarehouseImportMng.WarehouseImportSparepartDetail>()
                    .ForMember(d => d.WarehouseImportAreaDetails, o => o.MapFrom(s => s.WarehouseImportMng_WarehouseImportAreaDetail_View))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_WarehouseImportAreaDetail_View, DTO.WarehouseImportMng.WarehouseImportAreaDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_WarehouseImportColliDetail_View, DTO.WarehouseImportMng.WarehouseImportColliDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_WarehouseImport_View, DTO.WarehouseImportMng.WarehouseImport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImportedDate, o => o.ResolveUsing(s => s.ImportedDate.HasValue ? s.ImportedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ContainerReceivedDate, o => o.ResolveUsing(s => s.ContainerReceivedDate.HasValue ? s.ContainerReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ContainerReceivedTime, o => o.ResolveUsing(s => s.ContainerReceivedTime.HasValue ? s.ContainerReceivedTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.ResolveUsing(s => s.ConcurrencyFlag != null ? Convert.ToBase64String(s.ConcurrencyFlag) : ""))
                    .ForMember(d => d.Details, o => o.MapFrom(s => s.WarehouseImportMng_WarehouseImportProductDetail_View))
                    .ForMember(d => d.SparepartDetails, o => o.MapFrom(s => s.WarehouseImportMng_WarehouseImportSparepartDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WarehouseImportMng.WarehouseImportProductDetail, WarehouseImportProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseImportAreaDetail, o => o.Ignore())
                    .ForMember(d => d.WarehouseImportColliDetail, o => o.Ignore())
                    .ForMember(d => d.WarehouseImportProductDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WarehouseImportMng.WarehouseImportSparepartDetail, WarehouseImportSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseImportAreaDetail, o => o.Ignore())
                    .ForMember(d => d.WarehouseImportSparepartDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WarehouseImportMng.WarehouseImportAreaDetail, WarehouseImportAreaDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseImportAreaDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WarehouseImportMng.WarehouseImportColliDetail, WarehouseImportColliDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseImportColliDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WarehouseImportMng.WarehouseImport, WarehouseImport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.IsConfirmed, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.ImportedDate, o => o.Ignore())
                    .ForMember(d => d.ContainerReceivedDate, o => o.Ignore())
                    .ForMember(d => d.ContainerReceivedTime, o => o.Ignore())
                    .ForMember(d => d.WarehouseImportProductDetail, o => o.Ignore())
                    .ForMember(d => d.WarehouseImportSparepartDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_CreditNoteProduct_View, DTO.WarehouseImportMng.CreditNoteProduct>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_CreditNoteSparepart_View, DTO.WarehouseImportMng.CreditNoteSparepart>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_ProductColli_View, DTO.WarehouseImportMng.ProductColli>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseImportMng_ExportToWexData_View, DTO.WarehouseImportMng.ExportToWexData>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            
            }
        }
        
        public List<DTO.WarehouseImportMng.WarehouseImportSearchResult> DB2DTO_WarehouseImportSearchResultList(List<WarehouseImportMng_WarehouseImportSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseImportMng_WarehouseImportSearchResult_View>, List<DTO.WarehouseImportMng.WarehouseImportSearchResult>>(dbItems);
        }

        public List<DTO.WarehouseImportMng.OnSeaProduct> DB2DTO_OnSeaProductList(List<WarehouseImportMng_OnSeaProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseImportMng_OnSeaProduct_View>, List<DTO.WarehouseImportMng.OnSeaProduct>>(dbItems);
        }

        public List<DTO.WarehouseImportMng.OnSeaSparepart> DB2DTO_OnSeaSparepartList(List<WarehouseImportMng_OnSeaSparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseImportMng_OnSeaSparepart_View>, List<DTO.WarehouseImportMng.OnSeaSparepart>>(dbItems);
        }

        public DTO.WarehouseImportMng.WarehouseImport DB2DTO_WarehouseImport(WarehouseImportMng_WarehouseImport_View dbItem)
        {
            return AutoMapper.Mapper.Map<WarehouseImportMng_WarehouseImport_View, DTO.WarehouseImportMng.WarehouseImport>(dbItem);
        }

        public void DTO2DB(DTO.WarehouseImportMng.WarehouseImport dtoItem, ref WarehouseImport dbItem)
        {
            //Product
            List<WarehouseImportProductDetail> itemNeedDelete_Product = new List<WarehouseImportProductDetail>();
            List<WarehouseImportAreaDetail> itemNeedDelete_Area;
            List<WarehouseImportColliDetail> itemNeedDelete_ColliEANCode;
            if (dtoItem.Details != null)
            {
                //CHECK
                foreach (var dbProduct in dbItem.WarehouseImportProductDetail.ToArray())
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.Details.Select(s => s.WarehouseImportProductDetailID).Contains(dbProduct.WarehouseImportProductDetailID))
                    {
                        itemNeedDelete_Product.Add(dbProduct);
                    }
                    else //DB IS EXIST IN DB
                    {
                        //area
                        itemNeedDelete_Area = new List<WarehouseImportAreaDetail>();
                        foreach (var dbArea in dbProduct.WarehouseImportAreaDetail)
                        {
                            if (!dtoItem.Details.Where(o => o.WarehouseImportProductDetailID == dbProduct.WarehouseImportProductDetailID).FirstOrDefault().WarehouseImportAreaDetails.Select(x => x.WarehouseImportAreaDetailID).Contains(dbArea.WarehouseImportAreaDetailID))
                            {
                                itemNeedDelete_Area.Add(dbArea);
                            }
                        }
                        foreach (var dbArea in itemNeedDelete_Area)
                        {
                            dbProduct.WarehouseImportAreaDetail.Remove(dbArea);
                        }

                        //colli ean code
                        itemNeedDelete_ColliEANCode = new List<WarehouseImportColliDetail>();
                        foreach (var dbColli in dbProduct.WarehouseImportColliDetail)
                        {
                            if (!dtoItem.Details.Where(o => o.WarehouseImportProductDetailID == dbProduct.WarehouseImportProductDetailID).FirstOrDefault().WarehouseImportColliDetails.Select(x => x.WarehouseImportColliDetailID).Contains(dbColli.WarehouseImportColliDetailID))
                            {
                                itemNeedDelete_ColliEANCode.Add(dbColli);
                            }
                        }
                        foreach (var item in itemNeedDelete_ColliEANCode)
                        {
                            dbProduct.WarehouseImportColliDetail.Remove(item);
                        }
                    }
                }

                foreach (var dbProduct in itemNeedDelete_Product)
                {
                    //area
                    List<WarehouseImportAreaDetail> item_deleteds = new List<WarehouseImportAreaDetail>();
                    foreach (var dbArea in dbProduct.WarehouseImportAreaDetail)
                    {
                        item_deleteds.Add(dbArea);
                    }
                    foreach (var item in item_deleteds)
                    {
                        dbItem.WarehouseImportProductDetail.Where(o => o.WarehouseImportProductDetailID == dbProduct.WarehouseImportProductDetailID).FirstOrDefault().WarehouseImportAreaDetail.Remove(item);
                    }

                    //colli
                    List<WarehouseImportColliDetail> item_deleteds_colli = new List<WarehouseImportColliDetail>();
                    foreach (var item in dbProduct.WarehouseImportColliDetail)
                    {
                        item_deleteds_colli.Add(item);
                    }
                    foreach (var item in item_deleteds_colli)
                    {
                        dbItem.WarehouseImportProductDetail.Where(o => o.WarehouseImportProductDetailID == dbProduct.WarehouseImportProductDetailID).FirstOrDefault().WarehouseImportColliDetail.Remove(item);
                    }

                    //product
                    dbItem.WarehouseImportProductDetail.Remove(dbProduct);
                }

                //MAP
                WarehouseImportProductDetail _dbProduct;
                WarehouseImportAreaDetail _dbArea;
                WarehouseImportColliDetail _dbColli;
                foreach (var dtoProduct in dtoItem.Details)
                {
                    if (dtoProduct.WarehouseImportProductDetailID < 0)
                    {
                        _dbProduct = new WarehouseImportProductDetail();

                        //area
                        if (dtoProduct.WarehouseImportAreaDetails != null)
                        {
                            foreach (var dtoDetailExtend in dtoProduct.WarehouseImportAreaDetails)
                            {
                                _dbArea = new WarehouseImportAreaDetail();
                                _dbProduct.WarehouseImportAreaDetail.Add(_dbArea);
                                AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImportAreaDetail, WarehouseImportAreaDetail>(dtoDetailExtend, _dbArea);
                            }
                        }

                        //colli
                        if (dtoProduct.WarehouseImportColliDetails != null)
                        {
                            foreach (var item in dtoProduct.WarehouseImportColliDetails)
                            {
                                _dbColli = new WarehouseImportColliDetail();
                                _dbProduct.WarehouseImportColliDetail.Add(_dbColli);
                                AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImportColliDetail, WarehouseImportColliDetail>(item, _dbColli);
                            }
                        }

                        //product
                        dbItem.WarehouseImportProductDetail.Add(_dbProduct);
                    }
                    else
                    {
                        _dbProduct = dbItem.WarehouseImportProductDetail.FirstOrDefault(o => o.WarehouseImportProductDetailID == dtoProduct.WarehouseImportProductDetailID);
                        if (_dbProduct != null && dtoProduct.WarehouseImportAreaDetails != null)
                        {
                            //area
                            foreach (var dtoArea in dtoProduct.WarehouseImportAreaDetails)
                            {
                                if (dtoArea.WarehouseImportAreaDetailID < 0)
                                {
                                    _dbArea = new WarehouseImportAreaDetail();
                                    _dbProduct.WarehouseImportAreaDetail.Add(_dbArea);
                                }
                                else
                                {
                                    _dbArea = _dbProduct.WarehouseImportAreaDetail.FirstOrDefault(o => o.WarehouseImportAreaDetailID == dtoArea.WarehouseImportAreaDetailID);
                                }
                                if (_dbArea != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImportAreaDetail, WarehouseImportAreaDetail>(dtoArea, _dbArea);
                                }
                            }

                            //colli
                            foreach (var dtoColliItem in dtoProduct.WarehouseImportColliDetails)
                            {
                                if (dtoColliItem.WarehouseImportColliDetailID < 0)
                                {
                                    _dbColli = new WarehouseImportColliDetail();
                                    _dbProduct.WarehouseImportColliDetail.Add(_dbColli);
                                }
                                else
                                {
                                    _dbColli = _dbProduct.WarehouseImportColliDetail.FirstOrDefault(o => o.WarehouseImportColliDetailID == dtoColliItem.WarehouseImportColliDetailID);
                                }
                                if (_dbColli != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImportColliDetail, WarehouseImportColliDetail>(dtoColliItem, _dbColli);
                                }
                            }
                        }
                    }

                    if (_dbProduct != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImportProductDetail, WarehouseImportProductDetail>(dtoProduct, _dbProduct);
                    }
                }
            }

            //Sparepart
            List<WarehouseImportSparepartDetail> itemNeedDelete_Sparepart = new List<WarehouseImportSparepartDetail>();
            if (dtoItem.SparepartDetails != null)
            {
                //CHECK
                foreach (var dbSparepart in dbItem.WarehouseImportSparepartDetail)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.SparepartDetails.Select(s => s.WarehouseImportSparepartDetailID).Contains(dbSparepart.WarehouseImportSparepartDetailID))
                    {
                        itemNeedDelete_Sparepart.Add(dbSparepart);
                    }
                    else //DB IS EXIST IN DB
                    {
                        itemNeedDelete_Area = new List<WarehouseImportAreaDetail>();
                        foreach (var dbArea in dbSparepart.WarehouseImportAreaDetail)
                        {
                            if (!dtoItem.SparepartDetails.Where(o => o.WarehouseImportSparepartDetailID == dbSparepart.WarehouseImportSparepartDetailID).FirstOrDefault().WarehouseImportAreaDetails.Select(x => x.WarehouseImportAreaDetailID).Contains(dbArea.WarehouseImportAreaDetailID))
                            {
                                itemNeedDelete_Area.Add(dbArea);
                            }
                        }
                        foreach (var dbArea in itemNeedDelete_Area)
                        {
                            dbSparepart.WarehouseImportAreaDetail.Remove(dbArea);
                        }
                    }
                }

                foreach (var dbSparepart in itemNeedDelete_Sparepart)
                {
                    List<WarehouseImportAreaDetail> item_deleteds = new List<WarehouseImportAreaDetail>();
                    foreach (var dbArea in dbSparepart.WarehouseImportAreaDetail)
                    {
                        item_deleteds.Add(dbArea);
                    }
                    foreach (var item in item_deleteds)
                    {
                        dbItem.WarehouseImportSparepartDetail.Where(o => o.WarehouseImportSparepartDetailID == dbSparepart.WarehouseImportSparepartDetailID).FirstOrDefault().WarehouseImportAreaDetail.Remove(item);
                    }
                    dbItem.WarehouseImportSparepartDetail.Remove(dbSparepart);
                }

                //MAP
                WarehouseImportSparepartDetail _dbSparepart;
                WarehouseImportAreaDetail _dbArea;
                foreach (var dtoSparepart in dtoItem.SparepartDetails)
                {
                    if (dtoSparepart.WarehouseImportSparepartDetailID < 0)
                    {
                        _dbSparepart = new WarehouseImportSparepartDetail();

                        if (dtoSparepart.WarehouseImportAreaDetails != null)
                        {
                            foreach (var dtoArea in dtoSparepart.WarehouseImportAreaDetails)
                            {
                                _dbArea = new WarehouseImportAreaDetail();
                                _dbSparepart.WarehouseImportAreaDetail.Add(_dbArea);
                                AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImportAreaDetail, WarehouseImportAreaDetail>(dtoArea, _dbArea);
                            }
                        }

                        dbItem.WarehouseImportSparepartDetail.Add(_dbSparepart);
                    }
                    else
                    {
                        _dbSparepart = dbItem.WarehouseImportSparepartDetail.FirstOrDefault(o => o.WarehouseImportSparepartDetailID == dtoSparepart.WarehouseImportSparepartDetailID);
                        if (_dbSparepart != null && dtoSparepart.WarehouseImportAreaDetails != null)
                        {
                            foreach (var dtoArea in dtoSparepart.WarehouseImportAreaDetails)
                            {
                                if (dtoArea.WarehouseImportAreaDetailID < 0)
                                {
                                    _dbArea = new WarehouseImportAreaDetail();
                                    _dbSparepart.WarehouseImportAreaDetail.Add(_dbArea);
                                }
                                else
                                {
                                    _dbArea = _dbSparepart.WarehouseImportAreaDetail.FirstOrDefault(o => o.WarehouseImportAreaDetailID == dtoArea.WarehouseImportAreaDetailID);
                                }
                                if (_dbArea != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImportAreaDetail, WarehouseImportAreaDetail>(dtoArea, _dbArea);
                                }
                            }
                        }
                    }

                    if (_dbSparepart != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImportSparepartDetail, WarehouseImportSparepartDetail>(dtoSparepart, _dbSparepart);
                    }
                }
            }

            //mapping warehouse import
            AutoMapper.Mapper.Map<DTO.WarehouseImportMng.WarehouseImport, WarehouseImport>(dtoItem, dbItem);


        }

        public List<DTO.WarehouseImportMng.CreditNoteProduct> DB2DTO_CreditNoteProduct(List<WarehouseImportMng_CreditNoteProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseImportMng_CreditNoteProduct_View>, List<DTO.WarehouseImportMng.CreditNoteProduct>>(dbItems);
        }

        public List<DTO.WarehouseImportMng.CreditNoteSparepart> DB2DTO_CreditNoteSparepart(List<WarehouseImportMng_CreditNoteSparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseImportMng_CreditNoteSparepart_View>, List<DTO.WarehouseImportMng.CreditNoteSparepart>>(dbItems);
        }

        public List<DTO.WarehouseImportMng.ExportToWexData> DB2DTO_ShowDataExportToWex(List<WarehouseImportMng_ExportToWexData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseImportMng_ExportToWexData_View>, List<DTO.WarehouseImportMng.ExportToWexData>>(dbItems);
        }

        public List<DTO.WarehouseImportMng.ProductColli> DB2DTO_ProductColli(List<WarehouseImportMng_ProductColli_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseImportMng_ProductColli_View>, List<DTO.WarehouseImportMng.ProductColli>>(dbItems);
        }
    }
}
