using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ShowroomReceiptMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ShowroomReceiptMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ShowroomReceiptMng_ShowroomReceipt_SearchView, DTO.ShowroomReceiptMng.ShowroomReceiptSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomReceiptMng_ShowroomReceiptAreaDetail_View, DTO.ShowroomReceiptMng.ShowroomReceiptAreaDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                AutoMapper.Mapper.CreateMap<ShowroomReceiptMng_ShowroomReceiptDetail_View, DTO.ShowroomReceiptMng.ShowroomReceiptDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ShowroomReceiptAreaDetails, o => o.MapFrom(s => s.ShowroomReceiptMng_ShowroomReceiptAreaDetail_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomReceiptMng_ShowroomReceipt_View, DTO.ShowroomReceiptMng.ShowroomReceipt>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.ShowroomReceiptDetails, o => o.MapFrom(s => s.ShowroomReceiptMng_ShowroomReceiptDetail_View))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ShowroomReceiptMng.ShowroomReceipt, ShowroomReceipt>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ShowroomReceiptID, o => o.Ignore())
                   .ForMember(d => d.ShowroomReceiptDetail, o => o.Ignore())
                   .ForMember(d => d.ReceiptDate, o => o.Ignore())
                   .ForMember(d => d.CreatedDate, o => o.Ignore())
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.ShowroomReceiptMng.ShowroomReceiptDetail, ShowroomReceiptDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ShowroomReceiptDetailID, o => o.Ignore())
                  .ForMember(d => d.ShowroomReceiptAreaDetail, o => o.Ignore())
                  ;

                AutoMapper.Mapper.CreateMap<DTO.ShowroomReceiptMng.ShowroomReceiptAreaDetail, ShowroomReceiptAreaDetail>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.ShowroomReceiptAreaDetailID, o => o.Ignore())
                 ;

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<DTO.ShowroomReceiptMng.ShowroomReceiptSearch> DB2DTO_ShowroomReceiptSearch(List<ShowroomReceiptMng_ShowroomReceipt_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ShowroomReceiptMng_ShowroomReceipt_SearchView>, List<DTO.ShowroomReceiptMng.ShowroomReceiptSearch>>(dbItems);
        }

        public DTO.ShowroomReceiptMng.ShowroomReceipt DB2DTO_ShowroomReceipt(ShowroomReceiptMng_ShowroomReceipt_View dbItem)
        {
            return AutoMapper.Mapper.Map<ShowroomReceiptMng_ShowroomReceipt_View, DTO.ShowroomReceiptMng.ShowroomReceipt>(dbItem);
        }

        public void DTO2DB_ShowroomReceipt(DTO.ShowroomReceiptMng.ShowroomReceipt dtoItem, ref ShowroomReceipt dbItem)
        {
            //List<ShowroomReceiptDetail> product_tobedeleted = new List<ShowroomReceiptDetail>();
            //if (dtoItem.ShowroomReceiptDetails != null)
            //{
            //    //CHECK
            //    foreach (var dbDetail in dbItem.ShowroomReceiptDetail.Where(o => !dtoItem.ShowroomReceiptDetails.Select(s => s.ShowroomReceiptDetailID).Contains(o.ShowroomReceiptDetailID)))
            //    {
            //        product_tobedeleted.Add(dbDetail);
            //    }
            //    foreach (var dbDetail in product_tobedeleted)
            //    {
            //        dbItem.ShowroomReceiptDetail.Remove(dbDetail);
            //    }
            //    //MAP
            //    foreach (var dtoDetail in dtoItem.ShowroomReceiptDetails)
            //    {
            //        ShowroomReceiptDetail dbDetail;
            //        if (dtoDetail.ShowroomReceiptDetailID < 0)
            //        {
            //            dbDetail = new ShowroomReceiptDetail();
            //            dbItem.ShowroomReceiptDetail.Add(dbDetail);
            //        }
            //        else
            //        {
            //            dbDetail = dbItem.ShowroomReceiptDetail.FirstOrDefault(o => o.ShowroomReceiptDetailID == dtoDetail.ShowroomReceiptDetailID);
            //        }

            //        if (dbDetail != null)
            //        {
            //            AutoMapper.Mapper.Map<DTO.ShowroomReceiptMng.ShowroomReceiptDetail, ShowroomReceiptDetail>(dtoDetail, dbDetail);
            //        }
            //    }
            //}

            List<ShowroomReceiptDetail> itemNeedDelete_Product = new List<ShowroomReceiptDetail>();
            List<ShowroomReceiptAreaDetail> itemNeedDelete_Area;
            if (dtoItem.ShowroomReceiptDetails != null)
            {
                //CHECK
                foreach (var dbProduct in dbItem.ShowroomReceiptDetail)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.ShowroomReceiptDetails.Select(s => s.ShowroomReceiptDetailID).Contains(dbProduct.ShowroomReceiptDetailID))
                    {
                        itemNeedDelete_Product.Add(dbProduct);
                    }
                    else //DB IS EXIST IN DB
                    {
                        itemNeedDelete_Area = new List<ShowroomReceiptAreaDetail>();
                        foreach (var dbArea in dbProduct.ShowroomReceiptAreaDetail)
                        {
                            if (!dtoItem.ShowroomReceiptDetails.Where(o => o.ShowroomReceiptDetailID == dbProduct.ShowroomReceiptDetailID).FirstOrDefault().ShowroomReceiptAreaDetails.Select(x => x.ShowroomReceiptAreaDetailID).Contains(dbArea.ShowroomReceiptAreaDetailID))
                            {
                                itemNeedDelete_Area.Add(dbArea);
                            }
                        }
                        foreach (var dbArea in itemNeedDelete_Area)
                        {
                            dbProduct.ShowroomReceiptAreaDetail.Remove(dbArea);
                        }
                    }
                }

                foreach (var dbProduct in itemNeedDelete_Product)
                {
                    List<ShowroomReceiptAreaDetail> item_deleteds = new List<ShowroomReceiptAreaDetail>();
                    foreach (var dbArea in dbProduct.ShowroomReceiptAreaDetail)
                    {
                        item_deleteds.Add(dbArea);
                    }
                    foreach (var item in item_deleteds)
                    {
                        dbItem.ShowroomReceiptDetail.Where(o => o.ShowroomReceiptDetailID == dbProduct.ShowroomReceiptDetailID).FirstOrDefault().ShowroomReceiptAreaDetail.Remove(item);
                    }
                    dbItem.ShowroomReceiptDetail.Remove(dbProduct);
                }

                //MAP
                ShowroomReceiptDetail _dbProduct;
                ShowroomReceiptAreaDetail _dbArea;
                foreach (var dtoProduct in dtoItem.ShowroomReceiptDetails)
                {
                    if (dtoProduct.ShowroomReceiptDetailID < 0)
                    {
                        _dbProduct = new ShowroomReceiptDetail();

                        if (dtoProduct.ShowroomReceiptAreaDetails != null)
                        {
                            foreach (var dtoArea in dtoProduct.ShowroomReceiptAreaDetails)
                            {
                                _dbArea = new ShowroomReceiptAreaDetail();
                                _dbProduct.ShowroomReceiptAreaDetail.Add(_dbArea);
                                AutoMapper.Mapper.Map<DTO.ShowroomReceiptMng.ShowroomReceiptAreaDetail, ShowroomReceiptAreaDetail>(dtoArea, _dbArea);
                            }
                        }

                        dbItem.ShowroomReceiptDetail.Add(_dbProduct);
                    }
                    else
                    {
                        _dbProduct = dbItem.ShowroomReceiptDetail.FirstOrDefault(o => o.ShowroomReceiptDetailID == dtoProduct.ShowroomReceiptDetailID);
                        if (_dbProduct != null && dtoProduct.ShowroomReceiptAreaDetails != null)
                        {
                            foreach (var dtoArea in dtoProduct.ShowroomReceiptAreaDetails)
                            {
                                if (dtoArea.ShowroomReceiptAreaDetailID < 0)
                                {
                                    _dbArea = new ShowroomReceiptAreaDetail();
                                    _dbProduct.ShowroomReceiptAreaDetail.Add(_dbArea);
                                }
                                else
                                {
                                    _dbArea = _dbProduct.ShowroomReceiptAreaDetail.FirstOrDefault(o => o.ShowroomReceiptAreaDetailID == dtoArea.ShowroomReceiptAreaDetailID);
                                }
                                if (_dbArea != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ShowroomReceiptMng.ShowroomReceiptAreaDetail, ShowroomReceiptAreaDetail>(dtoArea, _dbArea);
                                }
                            }
                        }
                    }

                    if (_dbProduct != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ShowroomReceiptMng.ShowroomReceiptDetail, ShowroomReceiptDetail>(dtoProduct, _dbProduct);
                    }
                }
            }


            AutoMapper.Mapper.Map<DTO.ShowroomReceiptMng.ShowroomReceipt, ShowroomReceipt>(dtoItem, dbItem);
            if (dtoItem.ShowroomReceiptID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
            dbItem.ReceiptDate = dtoItem.ReceiptDate.ConvertStringToDateTime();
        }

    }
}
