using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Module.QAQCMng.DTO;

namespace Module.QAQCMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<QAQCMng_QAQC_View, QAQCSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.OrderDate, o => o.ResolveUsing(s => s.OrderDate.HasValue ? s.OrderDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCMng_function_SearchQAQC_Result, QAQCSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.OrderDate, o => o.ResolveUsing(s => s.OrderDate.HasValue ? s.OrderDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCMng_CheckList_View, CheckListDTO>()
                     .IgnoreAllNonExisting()                 
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCMng_Defects_View, DefectDTO>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCMng_ModelCheckListGroup_View, ModelCheckListGroupDTO>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCMng_ReportQAQC_GetStatus_View, QAQCReportStarusDTO>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Report Zone created by NL
                AutoMapper.Mapper.CreateMap<ReportQAQCMng_ReportQAQCSearch_View, DTO.ReportQAQCSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovalDate, o => o.ResolveUsing(s => s.ApprovalDate.HasValue ? s.ApprovalDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishedDate, o => o.ResolveUsing(s => s.FinishedDate.HasValue ? s.FinishedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ReportDate, o => o.ResolveUsing(s => s.ReportDate.HasValue ? s.ReportDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportQAQCMng_ReportQAQC_View, DTO.ReportQAQCDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovalDate, o => o.ResolveUsing(s => s.ApprovalDate.HasValue ? s.ApprovalDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishedDate, o => o.ResolveUsing(s => s.FinishedDate.HasValue ? s.FinishedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ReportDate, o => o.ResolveUsing(s => s.ReportDate.HasValue ? s.ReportDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.reportCheckLists, o => o.MapFrom(s => s.ReportQAQCMng_ReportCheckList_View))
                    .ForMember(d => d.reportDefects, o => o.MapFrom(s => s.ReportQAQCMng_ReportDefect_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportQAQCMng_ReportCheckList_View, DTO.ReportCheckListDTO>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.reportCheckListImages, o => o.MapFrom(s => s.ReportQAQCMng_ReportCheckListImage_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportQAQCMng_ReportDefect_View, DTO.ReportDefectDTO>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.reportDefectsImageDTOs, o => o.MapFrom(s => s.ReportQAQCMng_ReportDefectImage_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportQAQCMng_ReportCheckListImage_View, DTO.ReportCheckListImageDTO>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportQAQCMng_ReportDefectImage_View, DTO.ReportDefectsImageDTO>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QAQCMng_LogInspector_SearchView, LogInspectorDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SysDate, o => o.ResolveUsing(s => s.SysDate.HasValue ? s.SysDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<ModelCheckListGroupDTO> DB2DTO_ModelCheckListGroupDTOs(List<QAQCMng_ModelCheckListGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QAQCMng_ModelCheckListGroup_View>, List<ModelCheckListGroupDTO>>(dbItems);
        }
        public List<CheckListDTO> DB2DTO_CheckListDTOs(List<QAQCMng_CheckList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QAQCMng_CheckList_View>, List<CheckListDTO>>(dbItems);
        }
        public List<DefectDTO> DB2DTO_DefectDTOs(List<QAQCMng_Defects_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QAQCMng_Defects_View>, List<DefectDTO>>(dbItems);
        }
        public List<DTO.QAQCSearchResultDTO> DB2DTO_QAQCSearchResultDTOs(List<QAQCMng_function_SearchQAQC_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QAQCMng_function_SearchQAQC_Result>, List<DTO.QAQCSearchResultDTO>>(dbItems);
        }

        public List<DTO.QAQCSearchResultDTO> DB2DTO_QAQCSearch(List<QAQCMng_QAQC_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QAQCMng_QAQC_View>, List<DTO.QAQCSearchResultDTO>>(dbItems);
        }

        public DTO.ReportQAQCDTO BD2DTO_Report(ReportQAQCMng_ReportQAQC_View dbItem) {
            return AutoMapper.Mapper.Map<ReportQAQCMng_ReportQAQC_View, DTO.ReportQAQCDTO>(dbItem);
        }

        public List<DTO.ReportQAQCSearchDTO> DB2DTO_ReportSearch(List<ReportQAQCMng_ReportQAQCSearch_View> dbItems) {
            return AutoMapper.Mapper.Map<List<ReportQAQCMng_ReportQAQCSearch_View>, List<DTO.ReportQAQCSearchDTO>>(dbItems);
        }

        public List<DTO.LogInspectorDTO> DB2DTO_Loginspec(List<QAQCMng_LogInspector_SearchView> dbItems) {
            return AutoMapper.Mapper.Map<List<QAQCMng_LogInspector_SearchView>, List<DTO.LogInspectorDTO>>(dbItems);
        }

        //public DTO.SampleTechnicalDrawing DB2DTO_SampleTechnicalDrawing(Sample2Mng_SampleTechnicalDrawing_View dbItem)
        //{
        //    return AutoMapper.Mapper.Map<Sample2Mng_SampleTechnicalDrawing_View, DTO.SampleTechnicalDrawing>(dbItem);
        //}

        
    }
}
