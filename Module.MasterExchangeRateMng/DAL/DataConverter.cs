using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.MasterExchangeRateMng.DAL
{
    public class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MasterExchangeRateMng_MasterExchangeRate_SearchView, DTO.MasterExchangeRateSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidDate, o => o.ResolveUsing(s => s.ValidDate.HasValue ? s.ValidDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MasterExchangeRateMng_MasterExchangeRate_View, DTO.MasterExchangeRateEdit>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidDate, o => o.ResolveUsing(s => s.ValidDate.HasValue ? s.ValidDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //DB2DTO
                AutoMapper.Mapper.CreateMap<DTO.MasterExchangeRateEdit, MasterExchangeRate>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MasterExchangeRateID, o => o.Ignore())
                    .ForMember(d => d.ValidDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MasterExchangeRateSearch> DB2DTO_Search(List<MasterExchangeRateMng_MasterExchangeRate_SearchView>dbitems)
        {
            return AutoMapper.Mapper.Map<List<MasterExchangeRateMng_MasterExchangeRate_SearchView>, List<DTO.MasterExchangeRateSearch>>(dbitems);
        }

        public DTO.MasterExchangeRateEdit DB2DTO_EditView(MasterExchangeRateMng_MasterExchangeRate_View dbItem)
        {
            return AutoMapper.Mapper.Map<MasterExchangeRateMng_MasterExchangeRate_View, DTO.MasterExchangeRateEdit>(dbItem);
        }

        public void DTO2DB_UpdateData(DTO.MasterExchangeRateEdit dtoItem, ref MasterExchangeRate dbItem)
        {
            AutoMapper.Mapper.Map<DTO.MasterExchangeRateEdit, MasterExchangeRate>(dtoItem, dbItem);
            dbItem.ValidDate = dtoItem.ValidDate.ConvertStringToDateTime();
        }

    }
}
