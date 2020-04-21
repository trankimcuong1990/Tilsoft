using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.LabelingPackagingMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "LabelingPackagingMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProformaInvoiceList_View, DTO.LabelingPackagingMng.LabelingPackagingSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProformaInvoiceList_View, DTO.LabelingPackagingMng.LabelingPackaging>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackagingMng.LabelingPackaging, LabelingPackaging>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LabelingPackagingID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.LabelingPackagingMng.LabelingPackagingSearchResult> DB2DTO_LabelingPackagingSearchResultList(List<ProformaInvoiceList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProformaInvoiceList_View>, List<DTO.LabelingPackagingMng.LabelingPackagingSearchResult>>(dbItems);
        }
      
        public DTO.LabelingPackagingMng.LabelingPackaging DB2DTO_LabelingPackaging(ProformaInvoiceEdit_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProformaInvoiceEdit_View, DTO.LabelingPackagingMng.LabelingPackaging>(dbItem);
        }

        //public void DTO2BD_LabelingPackaging(DTO.LabelingPackagingMng.LabelingPackaging dtoItem, ref LabelingPackaging dbItem)
        //{
        //    AutoMapper.Mapper.Map<DTO.LabelingPackagingMng.LabelingPackaging, LabelingPackaging>(dtoItem, dbItem);
        //}

        public List<DTO.LabelingPackagingMng.PackagingMethod> DB2DTO_PackagingMethod(List<PackagingMethodList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PackagingMethodList_View>, List<DTO.LabelingPackagingMng.PackagingMethod>>(dbItems); 
        }

        public List<DTO.LabelingPackagingMng.PackagingType> DB2DTO_PackagingType(List<GetPackagingTypes_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<GetPackagingTypes_View>, List<DTO.LabelingPackagingMng.PackagingType>>(dbItems);
        }

        public List<DTO.LabelingPackagingMng.ProccessList> DB2DTO_ProccessList(List<GetLabalingPackagingProcessList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<GetLabalingPackagingProcessList_View>, List<DTO.LabelingPackagingMng.ProccessList>>(dbItems);
        }

        //public DTO.LabelingPackagingMng.ProccessList  GetSupportData(out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    DAL.Support.DataFactory factory = new Support.DataFactory();

        //    //try to get data
        //    try
        //    {
        //        DTO.LabelingPackagingMng.ProccessList dtoItem = new DTO.LabelingPackagingMng.ProccessList();
        //        dtoItem. = factory.GetSeason().ToList();
        //        return dtoItem;
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Library.DTO.Notification() { Message = ex.Message };
        //        return new DTO.LabelingPackagingMng.SupportData();
        //    }
        //}
        //public DTO.LabelingPackagingMng .Offer DB2DTO_Offer(OfferMng_Offer_View dbItem)
        //{

        //        dtoItem.OfferDateFormated = dbItem.OfferDate.Value.ToString("dd/MM/yyyy");

        //    if (dbItem.ValidUntil.HasValue)
        //        dtoItem.ValidUntilFormated = dbItem.ValidUntil.Value.ToString("dd/MM/yyyy");

        //    if (dbItem.LDS.HasValue)
        //        dtoItem.LDSFormated = dbItem.LDS.Value.ToString("dd/MM/yyyy");

        //    if (dbItem.EstimatedDeliveryDate.HasValue)
        //        dtoItem.EstimatedDeliveryDateFormated = dbItem.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy");

        //    return dtoItem;
        //}


    }
}
