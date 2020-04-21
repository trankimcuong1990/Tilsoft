using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryMaterialReceipt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryMaterialReceiptMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryMaterialReceiptMng_FactoryMaterialReceipt_SearchView, DTO.FactoryMaterialReceiptSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialReceiptMng_FactoryMaterialReceiptDetail_View, DTO.FactoryMaterialReceiptDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryMaterialReceiptDetails, o => o.MapFrom(s => s.FactoryMaterialReceiptMng_FactoryMaterialReceiptDetail_View1))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialReceiptMng_FactoryMaterialReceipt_View, DTO.FactoryMaterialReceipt>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactoryMaterialReceiptDetails, o => o.MapFrom(s => s.FactoryMaterialReceiptMng_FactoryMaterialReceiptDetail_View.Where(x =>!x.ParentID.HasValue)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialReceiptDetail, FactoryMaterialReceiptDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialReceiptDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialReceipt, FactoryMaterialReceipt>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryMaterialReceiptID, o => o.Ignore())
                    .ForMember(d => d.FactoryMaterialReceiptDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryMaterialReceiptMng_StockRemain_View, DTO.StockRemain>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialReceiptMng_NeedImportByOrder_View, DTO.NeedImportByOrder>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialReceiptMng_StockFree_View, DTO.StockFree>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryMaterialReceiptSearch> DB2DTO_FactoryMaterialReceiptSearch(List<FactoryMaterialReceiptMng_FactoryMaterialReceipt_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialReceiptMng_FactoryMaterialReceipt_SearchView>, List<DTO.FactoryMaterialReceiptSearch>>(dbItems);
        }

        public DTO.FactoryMaterialReceipt DB2DTO_FactoryMaterialReceipt(FactoryMaterialReceiptMng_FactoryMaterialReceipt_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryMaterialReceiptMng_FactoryMaterialReceipt_View, DTO.FactoryMaterialReceipt>(dbItem);
        }

        public void DTO2DB_FactoryMaterialReceipt(DTO.FactoryMaterialReceipt dtoItem, ref FactoryMaterialReceipt dbItem)
        {
            if (dtoItem.FactoryMaterialReceiptDetails != null) // allow user delete item in case receipt is export
            {
                //if (dtoItem.ReceiptTypeID == 2)
                //{
                //}
                foreach (var item in dbItem.FactoryMaterialReceiptDetail.Where(o => !o.ParentID.HasValue).ToArray())
                {
                    if (!dtoItem.FactoryMaterialReceiptDetails.Select(s => s.FactoryMaterialReceiptDetailID).Contains(item.FactoryMaterialReceiptDetailID))
                    {
                        foreach (var sItem in item.FactoryMaterialReceiptDetail1.ToArray())
                        {
                            item.FactoryMaterialReceiptDetail1.Remove(sItem);
                        }
                        dbItem.FactoryMaterialReceiptDetail.Remove(item);
                    }
                    else
                    {
                        foreach (var sItem in item.FactoryMaterialReceiptDetail1.ToArray())
                        {
                            var x = dtoItem.FactoryMaterialReceiptDetails.Where(o => o.FactoryMaterialReceiptDetailID == item.FactoryMaterialReceiptDetailID).FirstOrDefault().FactoryMaterialReceiptDetails;
                            if (!x.Select(s => s.FactoryMaterialReceiptDetailID).Contains(sItem.FactoryMaterialReceiptDetailID))
                            {
                                item.FactoryMaterialReceiptDetail1.Remove(sItem);
                            }
                        }
                    }
                }
                FactoryMaterialReceiptDetail dbDetail;
                FactoryMaterialReceiptDetail dbDetail1;
                foreach (var item in dtoItem.FactoryMaterialReceiptDetails)
                {
                    if (item.FactoryMaterialReceiptDetailID > 0)
                    {
                        dbDetail = dbItem.FactoryMaterialReceiptDetail.Where(o => o.FactoryMaterialReceiptDetailID == item.FactoryMaterialReceiptDetailID).FirstOrDefault();
                        if (dbDetail != null && item.FactoryMaterialReceiptDetails != null)
                        {
                            foreach (var mItem in item.FactoryMaterialReceiptDetails)
                            {
                                if (mItem.FactoryMaterialReceiptDetailID > 0)
                                {
                                    dbDetail1 = dbDetail.FactoryMaterialReceiptDetail1.Where(o => o.FactoryMaterialReceiptDetailID == mItem.FactoryMaterialReceiptDetailID).FirstOrDefault();
                                }
                                else
                                {
                                    dbDetail1 = new FactoryMaterialReceiptDetail();
                                    dbDetail.FactoryMaterialReceiptDetail1.Add(dbDetail1);
                                    dbItem.FactoryMaterialReceiptDetail.Add(dbDetail1);
                                }
                                if (dbDetail1 != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.FactoryMaterialReceiptDetail, FactoryMaterialReceiptDetail>(mItem, dbDetail1);
                                }
                            }
                        }
                    }
                    else
                    {
                        dbDetail = new FactoryMaterialReceiptDetail();
                        dbItem.FactoryMaterialReceiptDetail.Add(dbDetail);
                        if (item.FactoryMaterialReceiptDetails != null)
                        {
                            foreach (var mItem in item.FactoryMaterialReceiptDetails)
                            {
                                dbDetail1 = new FactoryMaterialReceiptDetail();
                                dbDetail.FactoryMaterialReceiptDetail1.Add(dbDetail1);
                                dbItem.FactoryMaterialReceiptDetail.Add(dbDetail1);
                                AutoMapper.Mapper.Map<DTO.FactoryMaterialReceiptDetail, FactoryMaterialReceiptDetail>(mItem, dbDetail1);
                            }
                        }
                    }
                    //mapping
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryMaterialReceiptDetail, FactoryMaterialReceiptDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryMaterialReceipt, FactoryMaterialReceipt>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.ReceiptDate = dtoItem.ReceiptDate.ConvertStringToDateTime();
            if (dtoItem.FactoryMaterialReceiptID == null)
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
        }

        public List<DTO.StockRemain> DB2DTO_StockRemain(List<FactoryMaterialReceiptMng_StockRemain_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialReceiptMng_StockRemain_View>, List<DTO.StockRemain>>(dbItems);
        }

        public List<DTO.NeedImportByOrder> DB2DTO_NeedImportByOrder(List<FactoryMaterialReceiptMng_NeedImportByOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialReceiptMng_NeedImportByOrder_View>, List<DTO.NeedImportByOrder>>(dbItems);
        }

        public List<DTO.StockFree> DB2DTO_StockFree(List<FactoryMaterialReceiptMng_StockFree_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialReceiptMng_StockFree_View>, List<DTO.StockFree>>(dbItems);
        }
    }
}
