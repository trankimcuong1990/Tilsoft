using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.MISaleByItemRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MISaleByItemRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MISaleByItemRpt_Top20ByCategory_View, DTO.Top20ByCategory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByItemRpt_Top30_View, DTO.Top30>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByItemRpt_ItemByClient_View, DTO.ItemByClient>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByItemRpt_CommercialInvoiceByCategories_View, DTO.CommercialInvoiceByCategories>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByItemRpt_function_GetPiConfirmedByItemCategory_Result, DTO.PiConfirmedByItemCategory>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.Top20ByCategory> DB2DTO_Top20ByCategoryList(List<MISaleByItemRpt_Top20ByCategory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByItemRpt_Top20ByCategory_View>, List<DTO.Top20ByCategory>>(dbItems);
        }
        public List<DTO.Top30> DB2DTO_Top30List(List<MISaleByItemRpt_Top30_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByItemRpt_Top30_View>, List<DTO.Top30>>(dbItems);
        }

        public List<DTO.ItemByClient> DB2DTO_ItemByClient(List<MISaleByItemRpt_ItemByClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByItemRpt_ItemByClient_View>, List<DTO.ItemByClient>>(dbItems);
        }

        public List<DTO.CommercialInvoiceByCategories> DB2DTO_CommercialInvoiceByCategories(List<MISaleByItemRpt_CommercialInvoiceByCategories_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByItemRpt_CommercialInvoiceByCategories_View>, List<DTO.CommercialInvoiceByCategories>>(dbItems);
        }


    }
}
