using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TestStandardMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "TestStandardMng";

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            //VIEW
            AutoMapper.Mapper.CreateMap<TestStandard_TestStandard_GetView, DTO.GetTestStandardDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));
           
            //DB2DTO
            AutoMapper.Mapper.CreateMap<DTO.TestStandardDTO, TestStandard>()
                .IgnoreAllNonExisting()
                .ForMember(s => s.TestStandardID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //GET VIEW
            AutoMapper.Mapper.CreateMap<TestStandard_TestStandard_View, DTO.TestStandardDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        //GET VIEW
        public DTO.TestStandardDTO DB2DTO_TestStandard(TestStandard_TestStandard_View dbItem)
        {
            return AutoMapper.Mapper.Map<TestStandard_TestStandard_View, DTO.TestStandardDTO>(dbItem);
        }

        public void DTO2DB_TestStandard(DTO.TestStandardDTO dtoItem, ref TestStandard dbItem)
        {
            AutoMapper.Mapper.Map<DTO.TestStandardDTO, TestStandard>(dtoItem, dbItem);
        }

        //VIEW
        public List<DTO.GetTestStandardDTO> DB2DTO_GetTestStandard(List<TestStandard_TestStandard_GetView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<TestStandard_TestStandard_GetView>, List<DTO.GetTestStandardDTO>>(dbItems);
        }

    }
}
