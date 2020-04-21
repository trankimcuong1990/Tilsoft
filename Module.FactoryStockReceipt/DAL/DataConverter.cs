using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryStockReceipt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryStockReceiptMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryStockReceiptMng_FactoryStockReceipt_SearchView, DTO.FactoryStockReceiptSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryStockReceiptMng_FactoryStockReceiptDetail_View, DTO.FactoryStockReceiptDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryStockReceiptMng_FactoryStockReceipt_View, DTO.FactoryStockReceipt>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactoryStockReceiptDetails, o => o.MapFrom(s => s.FactoryStockReceiptMng_FactoryStockReceiptDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryStockReceiptDetail, FactoryStockReceiptDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryStockReceiptDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryStockReceipt, FactoryStockReceipt>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryStockReceiptID, o => o.Ignore())
                    .ForMember(d => d.FactoryStockReceiptDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryStockReceiptMng_StockRemain_View, DTO.StockRemain>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryStockReceiptSearch> DB2DTO_FactoryStockReceiptSearch(List<FactoryStockReceiptMng_FactoryStockReceipt_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryStockReceiptMng_FactoryStockReceipt_SearchView>, List<DTO.FactoryStockReceiptSearch>>(dbItems);
        }

        public DTO.FactoryStockReceipt DB2DTO_FactoryStockReceipt(FactoryStockReceiptMng_FactoryStockReceipt_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryStockReceiptMng_FactoryStockReceipt_View, DTO.FactoryStockReceipt>(dbItem);
        }

        public void DTO2DB_FactoryStockReceipt(DTO.FactoryStockReceipt dtoItem, ref FactoryStockReceipt dbItem)
        {
            if (dtoItem.FactoryStockReceiptDetails != null) // allow user delete item in case receipt is export
            {
                foreach (var item in dbItem.FactoryStockReceiptDetail.ToArray())
                {
                    if (!dtoItem.FactoryStockReceiptDetails.Select(s => s.FactoryStockReceiptDetailID).Contains(item.FactoryStockReceiptDetailID))
                    {
                        dbItem.FactoryStockReceiptDetail.Remove(item);
                    }
                }

                foreach (var item in dtoItem.FactoryStockReceiptDetails)
                {
                    FactoryStockReceiptDetail dbDetail;
                    if (item.FactoryStockReceiptDetailID < 0)
                    {
                        dbDetail = new FactoryStockReceiptDetail();
                        dbItem.FactoryStockReceiptDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryStockReceiptDetail.Where(o => o.FactoryStockReceiptDetailID == item.FactoryStockReceiptDetailID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryStockReceiptDetail, FactoryStockReceiptDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryStockReceipt, FactoryStockReceipt>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.ReceiptDate = dtoItem.ReceiptDate.ConvertStringToDateTime();
            if (dtoItem.FactoryStockReceiptID == null)
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
        }

        public List<DTO.StockRemain> DB2DTO_StockRemain(List<FactoryStockReceiptMng_StockRemain_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryStockReceiptMng_StockRemain_View>, List<DTO.StockRemain>>(dbItems);
        }
    }
}
