using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.QAQCRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //

                AutoMapper.Mapper.CreateMap<QAQCRpt_InspectionProductivity_Function_Result, DTO.InspectionProductivityDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCRpt_TotalInspectionForFactoryMaster_Function_Result, DTO.TotalInspectionForFactoryMasterDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCRpt_TotalInspectionForFactoryDetail_Function_Result, DTO.TotalInspectionForFactoryDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductGroup_View, DTO.SupportProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCRpt_SupportClient_View, DTO.SupportClient>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCRpt_SupportFactory_View, DTO.SupportFactory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        //--------.>

        public List<DTO.InspectionProductivityDTO> DB2DTO_InspectionProductivity(List<QAQCRpt_InspectionProductivity_Function_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QAQCRpt_InspectionProductivity_Function_Result>, List<DTO.InspectionProductivityDTO>>(dbItems);
        }

        public List<DTO.TotalInspectionForFactoryMasterDTO> DB2DTO_InspectionForFactoryMaster(List<QAQCRpt_TotalInspectionForFactoryMaster_Function_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QAQCRpt_TotalInspectionForFactoryMaster_Function_Result>, List<DTO.TotalInspectionForFactoryMasterDTO>>(dbItems);
        }

        public List<DTO.TotalInspectionForFactoryDetailDTO> DB2DTO_InspectionForFactoryDetail(List<QAQCRpt_TotalInspectionForFactoryDetail_Function_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QAQCRpt_TotalInspectionForFactoryDetail_Function_Result>, List<DTO.TotalInspectionForFactoryDetailDTO>>(dbItems);
        }

    }
}
