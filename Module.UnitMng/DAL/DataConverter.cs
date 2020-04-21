using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.UnitMng.DAL
{
   public class DataConverter
    {

        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            AutoMapper.Mapper.CreateMap<UnitMng_UnitSearch_View, DTO.UnitMngSearchDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<UnitMng_Unit_View, DTO.UnitMngDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.UnitMngDto, Unit>()
                .ForMember(d => d.UnitID, o => o.Ignore())
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);
                
                
        }


        public List<DTO.UnitMngSearchDto> BD2DTO_UnitMngSearchResult(List<UnitMng_UnitSearch_View> items)
        {
            return AutoMapper.Mapper.Map<List<UnitMng_UnitSearch_View>, List<DTO.UnitMngSearchDto>>(items);
        }

        public DTO.UnitMngDto DB2DTO_UnitMng(UnitMng_Unit_View item)
        {
            return AutoMapper.Mapper.Map<UnitMng_Unit_View, DTO.UnitMngDto>(item);
        }

        public void DTO2DB_UnitMng(DTO.UnitMngDto dto, ref Unit db)
        {
            AutoMapper.Mapper.Map<DTO.UnitMngDto, Unit>(dto, db);
        }
    }
}
