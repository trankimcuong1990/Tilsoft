using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.VanPhatMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<VanPhatMng_DeliveryNote_SearchView, DTO.DeliveryNoteSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.deliveryNoteDetailSearches, o => o.MapFrom(s => s.VanPhatMng_DeliveryNoteDetail_SearchView))
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<VanPhatMng_DeliveryNoteDetail_SearchView, DTO.DeliveryNoteDetailSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DeliveryNoteSearch> DB2DTO_DeliveryNoteView(List<VanPhatMng_DeliveryNote_SearchView> items)
        {
            return AutoMapper.Mapper.Map<List<VanPhatMng_DeliveryNote_SearchView>, List<DTO.DeliveryNoteSearch>>(items);
        }

        public List<DTO.DeliveryNoteDetailSearch> DB2DTO_GetListDetail(List<VanPhatMng_DeliveryNoteDetail_SearchView> items)
        {
            return AutoMapper.Mapper.Map<List<VanPhatMng_DeliveryNoteDetail_SearchView>, List<DTO.DeliveryNoteDetailSearch>>(items);
        }

    }
}
