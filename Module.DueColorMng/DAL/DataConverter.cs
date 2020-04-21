using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DueColorMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DueColorMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DueColorMng_DueColor_SearchView, DTO.DueColorSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DueColorMng_DueColor_View, DTO.DueColorDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DueColorDTO, DueColor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DueColorID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DueColorSearchDTO> DB2DTO_DueColorSearchResultList(List<DueColorMng_DueColor_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DueColorMng_DueColor_SearchView>, List<DTO.DueColorSearchDTO>>(dbItems);
        }

        public DTO.DueColorDTO DB2DTO_DueColor(DueColorMng_DueColor_View dbItem)
        {
            return AutoMapper.Mapper.Map<DueColorMng_DueColor_View, DTO.DueColorDTO>(dbItem);
        }

        public void DTO2BD_DueColor(DTO.DueColorDTO dtoItem, ref DueColor dbItem)
        {
            AutoMapper.Mapper.Map<DTO.DueColorDTO, DueColor>(dtoItem, dbItem);
        }
    }
}
