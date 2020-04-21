using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookReceiptMng.DAL
{
    public class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");

        public DataConverter()
        {
            string mapName = "CashBookReceipt";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookType_View, DTO.CashBookTypeData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookSourceOfFlow_View, DTO.CashBookSourceOfFlowData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookLocation_View, DTO.CashBookLocationData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookPostCost_View, DTO.CashBookPostCostData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CashBookCostItemDatas, o => o.MapFrom(s => s.CashBookMng_CashBookCostItem_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookCostItem_View, DTO.CashBookCostItemData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CashBookCostItemDetailDatas, o => o.MapFrom(s => s.CashBookMng_CashBookCostItemDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookCostItemDetail_View, DTO.CashBookCostItemDetailData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookPaidBy_View, DTO.CashBookPaidByData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookPaidBy2_View, DTO.CashBookPaidByData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBook_View, DTO.CashBookData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BookDate, o => o.ResolveUsing(s => s.BookDate.HasValue ? s.BookDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CashBookDocuments, o => o.MapFrom(s => s.CashBookMng_CashBookDocument_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookMng_CashBookDocument_View, DTO.CashBookDocumentData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CashBookData, CashBook>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CashBookID, o => o.Ignore())
                    .ForMember(d => d.BookDate, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.BookDate) ? s.BookDate.ConvertStringToDateTime() : null))
                    //.ForMember(d => d.VNDAmount, o => o.ResolveUsing(s => s.VNDAmount.HasValue ? s.VNDAmount : null))
                    //.ForMember(d => d.VNDUSDExRate, o => o.ResolveUsing(s => s.VNDUSDExRate.HasValue ? s.VNDUSDExRate : null))
                    //.ForMember(d => d.CashBookPostCostID, o => o.Ignore())
                    //.ForMember(d => d.CashBookCostItemID, o => o.Ignore())
                    //.ForMember(d => d.CashBookCostItemDetailID, o => o.Ignore())
                    //.ForMember(d => d.CashBookPaidByID, o => o.Ignore())
                    //.ForMember(d => d.CashBookOtherCostItemDetail, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.CashBookOtherCostItemDetail) ? s.CashBookOtherCostItemDetail : null))
                    .ForMember(d => d.CashBookDocument, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.CashBookPostCostData, CashBookPostCost>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CashBookPostCostID, o => o.Ignore())
                    .ForMember(d => d.CashBookCostItem, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CashBookCostItemData, CashBookCostItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CashBookCostItemID, o => o.Ignore())
                    .ForMember(d => d.CashBookCostItemDetail, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CashBookCostItemDetailData, CashBookCostItemDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CashBookCostItemDetailID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CashBookDocumentData, CashBookDocument>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CashBookDocumentID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CashBookTypeData> DB2DTO_CashBookType(List<CashBookMng_CashBookType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookMng_CashBookType_View>, List<DTO.CashBookTypeData>>(dbItems);
        }

        public List<DTO.CashBookSourceOfFlowData> DB2DTO_CashBookSourceOfFlow(List<CashBookMng_CashBookSourceOfFlow_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookMng_CashBookSourceOfFlow_View>, List<DTO.CashBookSourceOfFlowData>>(dbItems);
        }

        public List<DTO.CashBookLocationData> DB2DTO_CashBookLocation(List<CashBookMng_CashBookLocation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookMng_CashBookLocation_View>, List<DTO.CashBookLocationData>>(dbItems);
        }

        public List<DTO.CashBookPaidByData> DB2DTO_CashBookPaidBy(List<CashBookMng_CashBookPaidBy2_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookMng_CashBookPaidBy2_View>, List<DTO.CashBookPaidByData>>(dbItems);
        }

        public DTO.CashBookPaidByData DB2DTO_CashBookPaidBy(CashBookMng_CashBookPaidBy_View dbItem)
        {
            return AutoMapper.Mapper.Map<CashBookMng_CashBookPaidBy_View, DTO.CashBookPaidByData>(dbItem);
        }

        public List<DTO.CashBookPostCostData> DB2DTO_CashBookPostCost(List<CashBookMng_CashBookPostCost_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookMng_CashBookPostCost_View>, List<DTO.CashBookPostCostData>>(dbItems);
        }

        public List<DTO.CashBookCostItemData> DB2DTO_CashBookCostItem(List<CashBookMng_CashBookCostItem_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookMng_CashBookCostItem_View>, List<DTO.CashBookCostItemData>>(dbItems);
        }

        public List<DTO.CashBookCostItemDetailData> DB2DTO_CashBookCostItemDetail(List<CashBookMng_CashBookCostItemDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookMng_CashBookCostItemDetail_View>, List<DTO.CashBookCostItemDetailData>>(dbItems);
        }

        public List<DTO.CashBookData> DB2DTO_CashBookSearchResult(List<CashBookMng_CashBook_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookMng_CashBook_View>, List<DTO.CashBookData>>(dbItems);
        }

        public DTO.CashBookData DB2DTO_CashBook(CashBookMng_CashBook_View dbItem)
        {
            return AutoMapper.Mapper.Map<CashBookMng_CashBook_View, DTO.CashBookData>(dbItem);
        }

        public void DTO2DB_CashBook(DTO.CashBookData dataItem, ref CashBook dbItem)
        {
            CashBookDocument dbDocument;

            foreach (CashBookDocument item in dbItem.CashBookDocument.ToArray())
            {
                if (!dataItem.CashBookDocuments.Select(s => s.CashBookDocumentID).Contains(item.CashBookDocumentID))
                {
                    dbItem.CashBookDocument.Remove(item);
                }
            }

            foreach (DTO.CashBookDocumentData dataDocument in dataItem.CashBookDocuments)
            {
                if (dataDocument.CashBookDocumentID <= 0)
                {
                    dbDocument = new CashBookDocument
                    {
                        FileUD = dataDocument.FileUD
                    };
                    dbItem.CashBookDocument.Add(dbDocument);
                }
                else
                {
                    dbDocument = dbItem.CashBookDocument.FirstOrDefault(o => o.CashBookDocumentID == dataDocument.CashBookDocumentID);
                }

                if (dbDocument != null)
                {
                    AutoMapper.Mapper.Map<DTO.CashBookDocumentData, CashBookDocument>(dataDocument, dbDocument);
                }
            }

            if (!string.IsNullOrEmpty(dataItem.BookDate))
            {
                if (DateTime.TryParse(dataItem.BookDate, nl, System.Globalization.DateTimeStyles.None, out DateTime tmpDate))
                {
                    dbItem.BookDate = tmpDate;
                }
            }

            AutoMapper.Mapper.Map<DTO.CashBookData, CashBook>(dataItem, dbItem);
        }

        public void DTO2DB_CashBookPostCost(DTO.CashBookPostCostData dataItem, ref CashBookPostCost dbItem)
        {
            AutoMapper.Mapper.Map<DTO.CashBookPostCostData, CashBookPostCost>(dataItem, dbItem);
        }

        public void DTO2DB_CashBookCostItem(DTO.CashBookCostItemData dataItem, ref CashBookCostItem dbItem)
        {
            AutoMapper.Mapper.Map<DTO.CashBookCostItemData, CashBookCostItem>(dataItem, dbItem);
        }

        public void DTO2DB_CashBookCostItemDetail(DTO.CashBookCostItemDetailData dataItem, ref CashBookCostItemDetail dbItem)
        {
            AutoMapper.Mapper.Map<DTO.CashBookCostItemDetailData, CashBookCostItemDetail>(dataItem, dbItem);
        }
    }
}
