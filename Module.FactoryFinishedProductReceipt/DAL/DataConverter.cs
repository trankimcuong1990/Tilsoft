using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryFinishedProductReceipt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryFinishedProductReceiptMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_SearchView, DTO.FactoryFinishedProductReceiptSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_FactoryFinishedProductReceiptDetail_View, DTO.FactoryFinishedProductReceiptDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_View, DTO.FactoryFinishedProductReceipt>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactoryFinishedProductReceiptDetails, o => o.MapFrom(s => s.FactoryFinishedProductReceiptMng_FactoryFinishedProductReceiptDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryFinishedProductReceiptDetail, FactoryFinishedProductReceiptDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryFinishedProductReceiptDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryFinishedProductReceipt, FactoryFinishedProductReceipt>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryFinishedProductReceiptID, o => o.Ignore())
                    .ForMember(d => d.FactoryFinishedProductReceiptDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_ComponentNeedToImport_View, DTO.ComponentNeedToImport>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_ComponentNeedToExport_View, DTO.ComponentNeedToExport>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_ComponentNeedToImport_WithoutOrders_BuyDirectly_View, DTO.ComponentNeedToImport_WithoutOrders_BuyDirectly>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_ComponentNeedToImport_WithoutOrders_InProgress_View, DTO.ComponentNeedToImport_WithoutOrders_InProgress>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_ComponentNeedToExport_WithoutOrders_InProgress_View, DTO.ComponentNeedToExport_WithoutOrders_InProgress>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductReceiptMng_ComponentNeedToImport_Orders_BuyDirectly_View, DTO.ComponentNeedToImport_Orders_BuyDirectly>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryFinishedProductReceiptSearch> DB2DTO_FactoryFinishedProductReceiptSearch(List<FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_SearchView>, List<DTO.FactoryFinishedProductReceiptSearch>>(dbItems);
        }

        public DTO.FactoryFinishedProductReceipt DB2DTO_FactoryFinishedProductReceipt(FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_View, DTO.FactoryFinishedProductReceipt>(dbItem);
        }

        public void DTO2DB_FactoryFinishedProductReceipt(DTO.FactoryFinishedProductReceipt dtoItem, ref FactoryFinishedProductReceipt dbItem)
        {
            if (dtoItem.FactoryFinishedProductReceiptDetails != null)
            {
                foreach (var item in dbItem.FactoryFinishedProductReceiptDetail.ToArray())
                {
                    if (!dtoItem.FactoryFinishedProductReceiptDetails.Select(s => s.FactoryFinishedProductReceiptDetailID).Contains(item.FactoryFinishedProductReceiptDetailID))
                    {
                        dbItem.FactoryFinishedProductReceiptDetail.Remove(item);
                    }
                }

                foreach (var item in dtoItem.FactoryFinishedProductReceiptDetails)
                {
                    FactoryFinishedProductReceiptDetail dbDetail;
                    if (item.FactoryFinishedProductReceiptDetailID < 0)
                    {
                        dbDetail = new FactoryFinishedProductReceiptDetail();
                        dbItem.FactoryFinishedProductReceiptDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryFinishedProductReceiptDetail.Where(o => o.FactoryFinishedProductReceiptDetailID == item.FactoryFinishedProductReceiptDetailID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryFinishedProductReceiptDetail, FactoryFinishedProductReceiptDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryFinishedProductReceipt, FactoryFinishedProductReceipt>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.ReceiptDate = dtoItem.ReceiptDate.ConvertStringToDateTime();
            if (dtoItem.FactoryFinishedProductReceiptID == null)
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
        }

        public List<DTO.ComponentNeedToImport> DB2DTO_ComponentNeedToImport(List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_View>, List<DTO.ComponentNeedToImport>>(dbItems);
        }

        public List<DTO.ComponentNeedToExport> DB2DTO_ComponentNeedToExport(List<FactoryFinishedProductReceiptMng_ComponentNeedToExport_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryFinishedProductReceiptMng_ComponentNeedToExport_View>, List<DTO.ComponentNeedToExport>>(dbItems);
        }

        public List<DTO.ComponentNeedToImport_WithoutOrders_BuyDirectly> DB2DTO_ComponentNeedToImport_WithoutOrdersBuyDirectly(List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_WithoutOrders_BuyDirectly_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_WithoutOrders_BuyDirectly_View>, List<DTO.ComponentNeedToImport_WithoutOrders_BuyDirectly>>(dbItems);
        }

        public List<DTO.ComponentNeedToImport_WithoutOrders_InProgress> DB2DTO_ComponentNeedToImport_WithoutOrdersInProgress(List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_WithoutOrders_InProgress_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_WithoutOrders_InProgress_View>, List<DTO.ComponentNeedToImport_WithoutOrders_InProgress>>(dbItems);
        }

        public List<DTO.ComponentNeedToExport_WithoutOrders_InProgress> DB2DTO_ComponentNeedToExport_WithoutOrdersInProgress(List<FactoryFinishedProductReceiptMng_ComponentNeedToExport_WithoutOrders_InProgress_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryFinishedProductReceiptMng_ComponentNeedToExport_WithoutOrders_InProgress_View>, List<DTO.ComponentNeedToExport_WithoutOrders_InProgress>>(dbItems);
        }

        public List<DTO.ComponentNeedToImport_Orders_BuyDirectly> DB2DTO_ComponentNeedToImportOrdersBuyDirectly(List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_Orders_BuyDirectly_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_Orders_BuyDirectly_View>, List<DTO.ComponentNeedToImport_Orders_BuyDirectly>>(dbItems);
        }
    }
}
