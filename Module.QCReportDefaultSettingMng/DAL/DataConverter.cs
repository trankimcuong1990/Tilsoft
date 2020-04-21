using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportDefaultSettingMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "QCReportDefaultSettingMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //SearchView
                AutoMapper.Mapper.CreateMap<QCReportDefaultSettingMng_QCReportDefaultSetting_SearchView, DTO.QCReportDefaultSettingSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Get view
                AutoMapper.Mapper.CreateMap<QCReportDefaultSettingMng_QCReportDefaultSetting_View, DTO.QCReportDefaultSettingDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QCReportDefaultSettingDTO, QCReportDefaultSetting>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QCReportDefaultSettingID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.QCReportDefaultSettingSearchResultDTO> DB2DTO_SearchQCReportDefaultSetting(List<QCReportDefaultSettingMng_QCReportDefaultSetting_SearchView> dbItem)
        {
            return AutoMapper.Mapper.Map<List<QCReportDefaultSettingMng_QCReportDefaultSetting_SearchView>, List<DTO.QCReportDefaultSettingSearchResultDTO>>(dbItem);
        }
        public DTO.QCReportDefaultSettingDTO DB2DTO_QCReportDefaultSetting(QCReportDefaultSettingMng_QCReportDefaultSetting_View dbItem)
        {
            return AutoMapper.Mapper.Map<QCReportDefaultSettingMng_QCReportDefaultSetting_View, DTO.QCReportDefaultSettingDTO>(dbItem);
        }

        public void DTO2DB_QCReportDefaultSetting(DTO.QCReportDefaultSettingDTO dtoItem, ref QCReportDefaultSetting dbItem)
        {
            AutoMapper.Mapper.Map<DTO.QCReportDefaultSettingDTO, QCReportDefaultSetting>(dtoItem, dbItem);
        }
    }
}
