using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLocationMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<FactoryLocationMng_Location_View, DTO.FactoryLocation>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.FactoryLocation, Location>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.LocationID, o => o.Ignore())
                .ForMember(d => d.LocationUD, o => o.Ignore());

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.FactoryLocation> DB2DTO_FactoryLocationSearchResult(List<FactoryLocationMng_Location_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactoryLocationMng_Location_View>, List<DTO.FactoryLocation>>(dbItem);
        }

        public DTO.FactoryLocation DB2DTO_FactoryLocation(FactoryLocationMng_Location_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryLocationMng_Location_View, DTO.FactoryLocation>(dbItem);
        }

        public void DTO2DB_UpdateData(DTO.FactoryLocation dtoItem, ref Location dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactoryLocation, Location>(dtoItem, dbItem);
        }
    }
}
