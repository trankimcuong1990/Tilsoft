using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.WarehousePickingListMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WarehousePickingList";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_WarehousePickingListSearchResult_View, DTO.WarehousePickingListMng.WarehousePickingListSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PlaningDate, o => o.ResolveUsing(s => s.PlaningDate.HasValue ? s.PlaningDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PlaningTime, o => o.ResolveUsing(s => s.PlaningTime.HasValue ? s.PlaningTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RealPickingDate, o => o.ResolveUsing(s => s.RealPickingDate.HasValue ? s.RealPickingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_RemainReservation_View, DTO.WarehousePickingListMng.RemainReservation>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_RemainSparepartReservation_View, DTO.WarehousePickingListMng.RemainSparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_WarehousePickingListProductDetail_View, DTO.WarehousePickingListMng.WarehousePickingListProductDetail>()
                    .ForMember(d => d.WarehousePickingListAreaDetails, o => o.MapFrom(s => s.WarehousePickingListMng_WarehousePickingListAreaDetail_View))
                    .ForMember(d => d.ModelPieces, o => o.MapFrom(s => s.WarehousePickingListMng_ModelPiece_View))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_WarehousePickingListSparepartDetail_View, DTO.WarehousePickingListMng.WarehousePickingListSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehousePickingListAreaDetails, o => o.MapFrom(s => s.WarehousePickingListMng_WarehousePickingListAreaDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_WarehousePickingListAreaDetail_View, DTO.WarehousePickingListMng.WarehousePickingListAreaDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_ModelPiece_View, DTO.WarehousePickingListMng.ModelPiece>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_WarehousePickingList_View, DTO.WarehousePickingListMng.WarehousePickingList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PlaningDate, o => o.ResolveUsing(s => s.PlaningDate.HasValue ? s.PlaningDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PlaningTime, o => o.ResolveUsing(s => s.PlaningTime.HasValue ? s.PlaningTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RealPickingDate, o => o.ResolveUsing(s => s.RealPickingDate.HasValue ? s.RealPickingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CMRDate, o => o.ResolveUsing(s => s.CMRDate.HasValue ? s.CMRDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DeliveryDate, o => o.ResolveUsing(s => s.DeliveryDate.HasValue ? s.DeliveryDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Details, o => o.MapFrom(s => s.WarehousePickingListMng_WarehousePickingListProductDetail_View))
                    .ForMember(d => d.SparepartDetails, o => o.MapFrom(s => s.WarehousePickingListMng_WarehousePickingListSparepartDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.WarehousePickingListMng.WarehousePickingListProductDetail, WarehousePickingListProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehousePickingListAreaDetail, o => o.Ignore())
                    .ForMember(d => d.WarehousePickingListProductDetailID, o => o.Ignore());
                    

                AutoMapper.Mapper.CreateMap<DTO.WarehousePickingListMng.WarehousePickingListSparepartDetail, WarehousePickingListSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehousePickingListAreaDetail, o => o.Ignore())
                    .ForMember(d => d.WarehousePickingListSparepartDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WarehousePickingListMng.WarehousePickingListAreaDetail, WarehousePickingListAreaDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehousePickingListAreaDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WarehousePickingListMng.WarehousePickingList, WarehousePickingList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProcessingStatusID, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.PlaningDate, o => o.Ignore())
                    .ForMember(d => d.PlaningTime, o => o.Ignore())
                    .ForMember(d => d.RealPickingDate, o => o.Ignore())
                    .ForMember(d => d.CMRDate, o => o.Ignore())
                    .ForMember(d => d.DeliveryDate, o => o.Ignore())
                    .ForMember(d => d.WarehousePickingListID, o => o.Ignore())
                    .ForMember(d => d.WarehousePickingListSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.WarehousePickingListProductDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<WarehousePickingListMng_PhysicalStockByWarehouseArea_View, DTO.WarehousePickingListMng.PhysicalStockByWarehouseArea>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<DTO.WarehousePickingListMng.WarehousePickingListSearchResult> DB2DTO_WarehousePickingListSearchResultList(List<WarehousePickingListMng_WarehousePickingListSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehousePickingListMng_WarehousePickingListSearchResult_View>, List<DTO.WarehousePickingListMng.WarehousePickingListSearchResult>>(dbItems);
        }

        public List<DTO.WarehousePickingListMng.RemainReservation> DB2DTO_RemainReservationList(List<WarehousePickingListMng_RemainReservation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehousePickingListMng_RemainReservation_View>, List<DTO.WarehousePickingListMng.RemainReservation>>(dbItems);
        }

        public List<DTO.WarehousePickingListMng.RemainSparepart> DB2DTO_RemainSparepartList(List<WarehousePickingListMng_RemainSparepartReservation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehousePickingListMng_RemainSparepartReservation_View>, List<DTO.WarehousePickingListMng.RemainSparepart>>(dbItems);
        }

        public DTO.WarehousePickingListMng.WarehousePickingList DB2DTO_WarehousePickingList(WarehousePickingListMng_WarehousePickingList_View dbItem)
        {
            return AutoMapper.Mapper.Map<WarehousePickingListMng_WarehousePickingList_View, DTO.WarehousePickingListMng.WarehousePickingList>(dbItem);
        }

        public void DTO2DB(DTO.WarehousePickingListMng.WarehousePickingList dtoItem, ref WarehousePickingList dbItem)
        {
            /*
            // map main object
            AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingList, WarehousePickingList>(dtoItem, dbItem);

            if (!string.IsNullOrEmpty(dtoItem.PlaningDate))
            {
                dbItem.PlaningDate = DateTime.ParseExact(dtoItem.PlaningDate, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            if (!string.IsNullOrEmpty(dtoItem.PlaningTime))
            {
                dbItem.PlaningTime = DateTime.ParseExact(dtoItem.PlaningTime, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            if (!string.IsNullOrEmpty(dtoItem.RealPickingDate))
            {
                dbItem.RealPickingDate = DateTime.ParseExact(dtoItem.RealPickingDate, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            if (!string.IsNullOrEmpty(dtoItem.CMRDate))
            {
                dbItem.CMRDate = DateTime.ParseExact(dtoItem.CMRDate, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            // product detail
            foreach (WarehousePickingListProductDetail toBeDeletedDetail in dbItem.WarehousePickingListProductDetail.ToArray())
            {
                if (!dtoItem.Details.Select(o => o.WarehousePickingListProductDetailID).Contains(toBeDeletedDetail.WarehousePickingListProductDetailID))
                {
                    dbItem.WarehousePickingListProductDetail.Remove(toBeDeletedDetail);
                }
            }
            foreach (DTO.WarehousePickingListMng.WarehousePickingListProductDetail dtoDetail in dtoItem.Details)
            {
                // add new case
                WarehousePickingListProductDetail dbDetail = null;
                if (dtoDetail.WarehousePickingListProductDetailID <= 0)
                {
                    dbDetail = new WarehousePickingListProductDetail();
                    dbDetail.WarehousePickingList = dbItem;
                    dbItem.WarehousePickingListProductDetail.Add(dbDetail);
                }
                else
                {
                    dbDetail = dbItem.WarehousePickingListProductDetail.FirstOrDefault(o => o.WarehousePickingListProductDetailID == dtoDetail.WarehousePickingListProductDetailID);
                }

                if (dbDetail != null)
                {
                    AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingListProductDetail, WarehousePickingListProductDetail>(dtoDetail, dbDetail);
                }
            }

            // sparepart detail
            foreach (WarehousePickingListSparepartDetail toBeDeletedSparepart in dbItem.WarehousePickingListSparepartDetail.ToArray())
            {
                if (!dtoItem.SparepartDetails.Select(o => o.WarehousePickingListSparepartDetailID).Contains(toBeDeletedSparepart.WarehousePickingListSparepartDetailID))
                {
                    dbItem.WarehousePickingListSparepartDetail.Remove(toBeDeletedSparepart);
                }
            }
            foreach (DTO.WarehousePickingListMng.WarehousePickingListSparepartDetail dtoSparepart in dtoItem.SparepartDetails)
            {
                // add new case
                WarehousePickingListSparepartDetail dbSparepart = null;
                if (dtoSparepart.WarehousePickingListSparepartDetailID <= 0)
                {
                    dbSparepart = new WarehousePickingListSparepartDetail();
                    dbItem.WarehousePickingListSparepartDetail.Add(dbSparepart);
                }
                else
                {
                    dbSparepart = dbItem.WarehousePickingListSparepartDetail.FirstOrDefault(o => o.WarehousePickingListSparepartDetailID == dtoSparepart.WarehousePickingListSparepartDetailID);
                }

                if (dbSparepart != null)
                {
                    AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingListSparepartDetail, WarehousePickingListSparepartDetail>(dtoSparepart, dbSparepart);
                }
            }

            */
            //Product
            List<WarehousePickingListProductDetail> itemNeedDelete_Product = new List<WarehousePickingListProductDetail>();
            List<WarehousePickingListAreaDetail> itemNeedDelete_Area;
            if (dtoItem.Details != null)
            {
                //CHECK
                foreach (var dbProduct in dbItem.WarehousePickingListProductDetail)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.Details.Select(s => s.WarehousePickingListProductDetailID).Contains(dbProduct.WarehousePickingListProductDetailID))
                    {
                        itemNeedDelete_Product.Add(dbProduct);
                    }
                    else //DB IS EXIST IN DB
                    {
                        itemNeedDelete_Area = new List<WarehousePickingListAreaDetail>();
                        foreach (var dbArea in dbProduct.WarehousePickingListAreaDetail)
                        {
                            if (!dtoItem.Details.Where(o => o.WarehousePickingListProductDetailID == dbProduct.WarehousePickingListProductDetailID).FirstOrDefault().WarehousePickingListAreaDetails.Select(x => x.WarehousePickingListAreaDetailID).Contains(dbArea.WarehousePickingListAreaDetailID))
                            {
                                itemNeedDelete_Area.Add(dbArea);
                            }
                        }
                        foreach (var dbArea in itemNeedDelete_Area)
                        {
                            dbProduct.WarehousePickingListAreaDetail.Remove(dbArea);
                        }
                    }
                }

                foreach (var dbProduct in itemNeedDelete_Product)
                {
                    List<WarehousePickingListAreaDetail> item_deleteds = new List<WarehousePickingListAreaDetail>();
                    foreach (var dbArea in dbProduct.WarehousePickingListAreaDetail)
                    {
                        item_deleteds.Add(dbArea);
                    }
                    foreach (var item in item_deleteds)
                    {
                        dbItem.WarehousePickingListProductDetail.Where(o => o.WarehousePickingListProductDetailID == dbProduct.WarehousePickingListProductDetailID).FirstOrDefault().WarehousePickingListAreaDetail.Remove(item);
                    }
                    dbItem.WarehousePickingListProductDetail.Remove(dbProduct);
                }

                //MAP
                WarehousePickingListProductDetail _dbProduct;
                WarehousePickingListAreaDetail _dbArea;
                foreach (var dtoProduct in dtoItem.Details)
                {
                    if (dtoProduct.WarehousePickingListProductDetailID < 0)
                    {
                        _dbProduct = new WarehousePickingListProductDetail();

                        if (dtoProduct.WarehousePickingListAreaDetails != null)
                        {
                            foreach (var dtoArea in dtoProduct.WarehousePickingListAreaDetails)
                            {
                                _dbArea = new WarehousePickingListAreaDetail();
                                _dbProduct.WarehousePickingListAreaDetail.Add(_dbArea);
                                AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingListAreaDetail, WarehousePickingListAreaDetail>(dtoArea, _dbArea);
                            }
                        }

                        dbItem.WarehousePickingListProductDetail.Add(_dbProduct);
                    }
                    else
                    {
                        _dbProduct = dbItem.WarehousePickingListProductDetail.FirstOrDefault(o => o.WarehousePickingListProductDetailID == dtoProduct.WarehousePickingListProductDetailID);
                        if (_dbProduct != null && dtoProduct.WarehousePickingListAreaDetails != null)
                        {
                            foreach (var dtoArea in dtoProduct.WarehousePickingListAreaDetails)
                            {
                                if (dtoArea.WarehousePickingListAreaDetailID < 0)
                                {
                                    _dbArea = new WarehousePickingListAreaDetail();
                                    _dbProduct.WarehousePickingListAreaDetail.Add(_dbArea);
                                }
                                else
                                {
                                    _dbArea = _dbProduct.WarehousePickingListAreaDetail.FirstOrDefault(o => o.WarehousePickingListAreaDetailID == dtoArea.WarehousePickingListAreaDetailID);
                                }
                                if (_dbArea != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingListAreaDetail, WarehousePickingListAreaDetail>(dtoArea, _dbArea);
                                }
                            }
                        }
                    }

                    if (_dbProduct != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingListProductDetail, WarehousePickingListProductDetail>(dtoProduct, _dbProduct);
                    }
                }
            }

            //Sparepart
            List<WarehousePickingListSparepartDetail> itemNeedDelete_Sparepart = new List<WarehousePickingListSparepartDetail>();
            if (dtoItem.SparepartDetails != null)
            {
                //CHECK
                foreach (var dbSparepart in dbItem.WarehousePickingListSparepartDetail)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.SparepartDetails.Select(s => s.WarehousePickingListSparepartDetailID).Contains(dbSparepart.WarehousePickingListSparepartDetailID))
                    {
                        itemNeedDelete_Sparepart.Add(dbSparepart);
                    }
                    else //DB IS EXIST IN DB
                    {
                        itemNeedDelete_Area = new List<WarehousePickingListAreaDetail>();
                        foreach (var dbArea in dbSparepart.WarehousePickingListAreaDetail)
                        {
                            if (!dtoItem.SparepartDetails.Where(o => o.WarehousePickingListSparepartDetailID == dbSparepart.WarehousePickingListSparepartDetailID).FirstOrDefault().WarehousePickingListAreaDetails.Select(x => x.WarehousePickingListAreaDetailID).Contains(dbArea.WarehousePickingListAreaDetailID))
                            {
                                itemNeedDelete_Area.Add(dbArea);
                            }
                        }
                        foreach (var dbArea in itemNeedDelete_Area)
                        {
                            dbSparepart.WarehousePickingListAreaDetail.Remove(dbArea);
                        }
                    }
                }

                foreach (var dbSparepart in itemNeedDelete_Sparepart)
                {
                    List<WarehousePickingListAreaDetail> item_deleteds = new List<WarehousePickingListAreaDetail>();
                    foreach (var dbArea in dbSparepart.WarehousePickingListAreaDetail)
                    {
                        item_deleteds.Add(dbArea);
                    }
                    foreach (var item in item_deleteds)
                    {
                        dbItem.WarehousePickingListSparepartDetail.Where(o => o.WarehousePickingListSparepartDetailID == dbSparepart.WarehousePickingListSparepartDetailID).FirstOrDefault().WarehousePickingListAreaDetail.Remove(item);
                    }
                    dbItem.WarehousePickingListSparepartDetail.Remove(dbSparepart);
                }

                //MAP
                WarehousePickingListSparepartDetail _dbSparepart;
                WarehousePickingListAreaDetail _dbArea;
                foreach (var dtoSparepart in dtoItem.SparepartDetails)
                {
                    if (dtoSparepart.WarehousePickingListSparepartDetailID < 0)
                    {
                        _dbSparepart = new WarehousePickingListSparepartDetail();

                        if (dtoSparepart.WarehousePickingListAreaDetails != null)
                        {
                            foreach (var dtoArea in dtoSparepart.WarehousePickingListAreaDetails)
                            {
                                _dbArea = new WarehousePickingListAreaDetail();
                                _dbSparepart.WarehousePickingListAreaDetail.Add(_dbArea);
                                AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingListAreaDetail, WarehousePickingListAreaDetail>(dtoArea, _dbArea);
                            }
                        }

                        dbItem.WarehousePickingListSparepartDetail.Add(_dbSparepart);
                    }
                    else
                    {
                        _dbSparepart = dbItem.WarehousePickingListSparepartDetail.FirstOrDefault(o => o.WarehousePickingListSparepartDetailID == dtoSparepart.WarehousePickingListSparepartDetailID);
                        if (_dbSparepart != null && dtoSparepart.WarehousePickingListAreaDetails != null)
                        {
                            foreach (var dtoArea in dtoSparepart.WarehousePickingListAreaDetails)
                            {
                                if (dtoArea.WarehousePickingListAreaDetailID < 0)
                                {
                                    _dbArea = new WarehousePickingListAreaDetail();
                                    _dbSparepart.WarehousePickingListAreaDetail.Add(_dbArea);
                                }
                                else
                                {
                                    _dbArea = _dbSparepart.WarehousePickingListAreaDetail.FirstOrDefault(o => o.WarehousePickingListAreaDetailID == dtoArea.WarehousePickingListAreaDetailID);
                                }
                                if (_dbArea != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingListAreaDetail, WarehousePickingListAreaDetail>(dtoArea, _dbArea);
                                }
                            }
                        }
                    }

                    if (_dbSparepart != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingListSparepartDetail, WarehousePickingListSparepartDetail>(dtoSparepart, _dbSparepart);
                    }
                }
            }

            //mapping warehouse import
            AutoMapper.Mapper.Map<DTO.WarehousePickingListMng.WarehousePickingList, WarehousePickingList>(dtoItem, dbItem);

            if (!string.IsNullOrEmpty(dtoItem.PlaningDate))
            {
                dbItem.PlaningDate = DateTime.ParseExact(dtoItem.PlaningDate, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            if (!string.IsNullOrEmpty(dtoItem.PlaningTime))
            {
                dbItem.PlaningTime = DateTime.ParseExact(dtoItem.PlaningTime, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            if (!string.IsNullOrEmpty(dtoItem.RealPickingDate))
            {
                dbItem.RealPickingDate = DateTime.ParseExact(dtoItem.RealPickingDate, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            if (!string.IsNullOrEmpty(dtoItem.CMRDate))
            {
                dbItem.CMRDate = DateTime.ParseExact(dtoItem.CMRDate, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            if (!string.IsNullOrEmpty(dtoItem.DeliveryDate))
            {
                dbItem.DeliveryDate = DateTime.ParseExact(dtoItem.DeliveryDate, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

        }

        public DTO.WarehousePickingListMng.PickingListContainerPrintout DB2DTO_PickingListPrintout(WarehousePickingListMng_PickingList_ReportView dbItem)
        {
            DTO.WarehousePickingListMng.PickingListContainerPrintout dtoItem = new DTO.WarehousePickingListMng.PickingListContainerPrintout();
            dtoItem.PickingListPrintouts = new List<DTO.WarehousePickingListMng.PickingListPrintout>();
            dtoItem.PickingListDetailPrintouts = new List<DTO.WarehousePickingListMng.PickingListDetailPrintout>();

            //READ PICKIGLIST
            DTO.WarehousePickingListMng.PickingListPrintout dtoPickingList = new DTO.WarehousePickingListMng.PickingListPrintout();
            dtoPickingList.ClientNM = dbItem.ClientNM + "      " + dbItem.ClientUD;
            dtoPickingList.Address = dbItem.Address;
            dtoPickingList.OrderNo = dbItem.OrderNo;
            dtoPickingList.RealPickingDate = dbItem.RealPickingDate;
            dtoPickingList.Remark = dbItem.Remark;
            dtoPickingList.RefNo = dbItem.RefNo;
            dtoPickingList.DeliveryDate = dbItem.DeliveryDate;
            dtoPickingList.TotalPickedQnt = dbItem.WarehousePickingListMng_PickingListAreaDetail_ReportView.Sum(o =>o.PickedQnt);
            dtoPickingList.ReceiptNo = dbItem.ReceiptNo;
            dtoItem.PickingListPrintouts.Add(dtoPickingList);

            //READ PICKING LIST DETAIL
            DTO.WarehousePickingListMng.PickingListDetailPrintout dtoPickingDetail;
            foreach (var item in dbItem.WarehousePickingListMng_PickingListAreaDetail_ReportView)
            {
                dtoPickingDetail = new DTO.WarehousePickingListMng.PickingListDetailPrintout();
                dtoPickingDetail.ArticleCode = item.ArticleCode;
                dtoPickingDetail.Description = item.Description;
                dtoPickingDetail.WarehouseAreaUD = item.WarehouseAreaUD;
                dtoPickingDetail.Quantity = item.Quantity;
                dtoPickingDetail.PickedQnt = item.PickedQnt;
                dtoPickingDetail.Unit = item.Unit;
                dtoPickingDetail.IsChecked = (item.IsChecked.HasValue && (bool)item.IsChecked ? "YES" : "NO");
                dtoItem.PickingListDetailPrintouts.Add(dtoPickingDetail);

                if (!string.IsNullOrEmpty(item.Remark))
                {
                    dtoPickingDetail = new DTO.WarehousePickingListMng.PickingListDetailPrintout();
                    dtoPickingDetail.Description = item.Remark;
                    dtoItem.PickingListDetailPrintouts.Add(dtoPickingDetail);
                }
            }
            //RETURN DATA
            return dtoItem;
        }

        public DTO.WarehousePickingListMng.CMRContainer DB2DTO_CMR(WarehousePickingListMng_PickingList_ReportView dbItem)
        {
            DTO.WarehousePickingListMng.CMRContainer dtoItem = new DTO.WarehousePickingListMng.CMRContainer();
            dtoItem.CMRs = new List<DTO.WarehousePickingListMng.CMR>();
            dtoItem.CMRDetails = new List<DTO.WarehousePickingListMng.CMRDetail>();

            //READ PICKING LIST DETAIL
            DTO.WarehousePickingListMng.CMRDetail dtoCMRDetail;
            int i = 1;
            int? TotalQnt = 0;
            foreach (var item in dbItem.WarehousePickingListMng_PickingListProductDetail_ReportView)
            {
                dtoCMRDetail = new DTO.WarehousePickingListMng.CMRDetail();
                dtoCMRDetail.RowIndex = i;
                if (item.Description.Length > 90)
                {
                    dtoCMRDetail.Description = item.Description.Substring(0, 85) + "..........";
                }
                else
                {
                    dtoCMRDetail.Description = item.Description;
                }

                dtoCMRDetail.Quantity = item.Quantity;
                dtoItem.CMRDetails.Add(dtoCMRDetail);
                i++;
                TotalQnt += item.Quantity;
            }
            int maxRow = 17; //(that mean 16 product)
            if (i < maxRow)
            {
                for (int j = 1; j <= maxRow - i; j++)
                {
                    dtoCMRDetail = new DTO.WarehousePickingListMng.CMRDetail();
                    dtoCMRDetail.Description = "";
                    //dtoCMRDetail.RowIndex = j;
                    dtoItem.CMRDetails.Add(dtoCMRDetail);
                }
            }

            //READ PICKIGLIST
            DTO.WarehousePickingListMng.CMR dtoCMR = new DTO.WarehousePickingListMng.CMR();
            dtoCMR.CMR1 = dbItem.CMR1;
            dtoCMR.CMR2 = dbItem.CMR2;
            dtoCMR.CMR3 = dbItem.CMR3;
            dtoCMR.CMR4 = dbItem.CMR4;
            dtoCMR.CMR5 = dbItem.CMR5;
            dtoCMR.CMR13 = dbItem.CMR13;
            dtoCMR.CMR14 = dbItem.CMR14;
            dtoCMR.CMR15 = dbItem.CMR15;
            dtoCMR.CMR16 = dbItem.CMR16;
            dtoCMR.CMR17 = dbItem.CMR17;
            dtoCMR.CMR18 = dbItem.CMR18;
            dtoCMR.CMR19 = dbItem.CMR19;
            dtoCMR.CMR20 = dbItem.CMR20;
            dtoCMR.CMR21 = dbItem.CMR21;
            dtoCMR.CMR22 = dbItem.CMR22;
            dtoCMR.CMR23 = dbItem.CMR23;
            dtoCMR.CMR24 = dbItem.CMR24;

            dtoCMR.TotalQnt = TotalQnt;
            dtoCMR.CMRDate = dbItem.CMRDate;
            dtoItem.CMRs.Add(dtoCMR);

            //RETURN DATA
            return dtoItem;
        }

        public List<DTO.WarehousePickingListMng.PhysicalStockByWarehouseArea> DB2DTO_PhysicalStockByWarehouseArea(List<WarehousePickingListMng_PhysicalStockByWarehouseArea_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehousePickingListMng_PhysicalStockByWarehouseArea_View>, List<DTO.WarehousePickingListMng.PhysicalStockByWarehouseArea>>(dbItems);
        }
    }
}
