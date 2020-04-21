using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.UsageTrackingRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //

                AutoMapper.Mapper.CreateMap<UsageTrackingMng_Module_View, DTO.Module>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_ActiveUser_View, DTO.ActiveUser>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_ActiveModule_View, DTO.ActiveModule>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_ActiveIP_View, DTO.ActiveIP>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_ActiveCompany_View, DTO.ActiveCompany>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_ActiveCompanyWeeklyHit_View, DTO.ActiveCompanyWeeklyHit>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_ActiveBrowser_View, DTO.ActiveBrowser>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_UserActionDetail_View, DTO.UserDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_ModuleActionDetail_View, DTO.ModuleDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_UserActionDetailWeeklyDetail_View, DTO.UserDetailWeeklyDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_UserActionDetailWeeklyOverview_View, DTO.UserDetailWeeklyOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_CompanyActionDetail_View, DTO.CompanyDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_CompanyActionDetailWeeklyDetail_View, DTO.CompanyDetailWeeklyDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_CompanyActionDetailWeeklyOverview_View, DTO.CompanyDetailWeeklyOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_HitCountByWeek_View, DTO.HitCount>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UsageTrackingRpt_UserIncreasingByWeek_View, DTO.UserMutation>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.Module> DB2DTO_ModuleList(List<UsageTrackingMng_Module_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingMng_Module_View>, List<DTO.Module>>(dbItems);
        }

        public List<DTO.ActiveUser> DB2DTO_ActiveUserList(List<UsageTrackingRpt_ActiveUser_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_ActiveUser_View>, List<DTO.ActiveUser>>(dbItems);
        }

        public List<DTO.ActiveModule> DB2DTO_ActiveModuleList(List<UsageTrackingRpt_ActiveModule_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_ActiveModule_View>, List<DTO.ActiveModule>>(dbItems);
        }

        public List<DTO.ActiveIP> DB2DTO_ActiveIPList(List<UsageTrackingRpt_ActiveIP_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_ActiveIP_View>, List<DTO.ActiveIP>>(dbItems);
        }

        public List<DTO.ActiveCompany> DB2DTO_ActiveCompanyList(List<UsageTrackingRpt_ActiveCompany_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_ActiveCompany_View>, List<DTO.ActiveCompany>>(dbItems);
        }

        public List<DTO.ActiveCompanyWeeklyHit> DB2DTO_ActiveCompanyWeeklyHitList(List<UsageTrackingRpt_ActiveCompanyWeeklyHit_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_ActiveCompanyWeeklyHit_View>, List<DTO.ActiveCompanyWeeklyHit>>(dbItems);
        }

        public List<DTO.ActiveBrowser> DB2DTO_ActiveBrowserList(List<UsageTrackingRpt_ActiveBrowser_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_ActiveBrowser_View>, List<DTO.ActiveBrowser>>(dbItems);
        }

        public List<DTO.UserDetail> DB2DTO_UserDetailList(List<UsageTrackingRpt_UserActionDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_UserActionDetail_View>, List<DTO.UserDetail>>(dbItems);
        }

        public List<DTO.ModuleDetail> DB2DTO_ModuleDetailList(List<UsageTrackingRpt_ModuleActionDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_ModuleActionDetail_View>, List<DTO.ModuleDetail>>(dbItems);
        }

        public List<DTO.UserDetailWeeklyDetail> DB2DTO_UserDetailWeeklyDetailList(List<UsageTrackingRpt_UserActionDetailWeeklyDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_UserActionDetailWeeklyDetail_View>, List<DTO.UserDetailWeeklyDetail>>(dbItems);
        }

        public List<DTO.UserDetailWeeklyOverview> DB2DTO_UserDetailWeeklyOverviewList(List<UsageTrackingRpt_UserActionDetailWeeklyOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_UserActionDetailWeeklyOverview_View>, List<DTO.UserDetailWeeklyOverview>>(dbItems);
        }

        public List<DTO.CompanyDetail> DB2DTO_CompanyDetailList(List<UsageTrackingRpt_CompanyActionDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_CompanyActionDetail_View>, List<DTO.CompanyDetail>>(dbItems);
        }

        public List<DTO.CompanyDetailWeeklyDetail> DB2DTO_CompanyDetailWeeklyDetailList(List<UsageTrackingRpt_CompanyActionDetailWeeklyDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_CompanyActionDetailWeeklyDetail_View>, List<DTO.CompanyDetailWeeklyDetail>>(dbItems);
        }

        public List<DTO.CompanyDetailWeeklyOverview> DB2DTO_CompanyDetailWeeklyOverviewList(List<UsageTrackingRpt_CompanyActionDetailWeeklyOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_CompanyActionDetailWeeklyOverview_View>, List<DTO.CompanyDetailWeeklyOverview>>(dbItems);
        }

        public List<DTO.HitCount> DB2DTO_HitCountList(List<UsageTrackingRpt_HitCountByWeek_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_HitCountByWeek_View>, List<DTO.HitCount>>(dbItems);
        }

        public List<DTO.UserMutation> DB2DTO_UserMutationList(List<UsageTrackingRpt_UserIncreasingByWeek_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UsageTrackingRpt_UserIncreasingByWeek_View>, List<DTO.UserMutation>>(dbItems);
        }
    }
}
