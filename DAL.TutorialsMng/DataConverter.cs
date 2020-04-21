using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.TutorialsMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "TutorialsMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //AutoMapper.Mapper.CreateMap<Tutorials, DTO.Tutorials.Tutorials>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TutorialsList_View, DTO.Tutorials.Tutorials>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.Tutorials.Tutorials, Tutorials >()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.Tutorials.Tutorials> DB2DTO_TutorialSearchResultList(List<TutorialsList_View > dbItems)
        {
            return AutoMapper.Mapper.Map<List<TutorialsList_View >, List<DTO.Tutorials.Tutorials>>(dbItems);
        }

        public DTO.Tutorials.Tutorials DB2DTO_Tutorial(TutorialsList_View   dbItem)
        {
            return AutoMapper.Mapper.Map<TutorialsList_View , DTO.Tutorials.Tutorials>(dbItem);
        }

        public void DTO2BD_Tutorial(DTO.Tutorials.Tutorials dtoItem, ref Tutorials dbItem)
        {
            AutoMapper.Mapper.Map<DTO.Tutorials.Tutorials, Tutorials >(dtoItem, dbItem);
        }



        //public DataConverter()
        //{
        //    string mapName = "TutorialsMng";
        //    if (!FrameworkSetting.Setting.Maps.Contains(mapName))
        //    {
        //        AutoMapper.Mapper.CreateMap<Tutorials, DTO.Tutorials.Tutorials>()
        //            .IgnoreAllNonExisting()
        //            .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

        //        //AutoMapper.Mapper.CreateMap<TutorialsList_View , DTO.Tutorials .Tutorials >()
        //        //    .IgnoreAllNonExisting()
        //        //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

        //        AutoMapper.Mapper.CreateMap<DTO.Tutorials.Tutorials, Tutorials>()
        //            .IgnoreAllNonExisting()
        //            .ForMember(d => d.ID, o => o.Ignore());

        //        FrameworkSetting.Setting.Maps.Add(mapName);
        //    }
        //}

        //public List<DTO.Tutorials.Tutorials> DB2DTO_TutorialSearchResultList(List<Tutorials> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<Tutorials>, List<DTO.Tutorials.Tutorials>>(dbItems);
        //}

        //public DTO.Tutorials.Tutorials DB2DTO_Tutorial(Tutorials dbItem)
        //{
        //    return AutoMapper.Mapper.Map<Tutorials, DTO.Tutorials.Tutorials>(dbItem);
        //}

        //public void DTO2BD_Tutorial(DTO.Tutorials.Tutorials dtoItem, ref Tutorials dbItem)
        //{
        //    AutoMapper.Mapper.Map<DTO.Tutorials.Tutorials, Tutorials>(dtoItem, dbItem);
        //}
    }
}
