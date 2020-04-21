using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.DevRequestMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<DevRequestMng_DevRequestSearchResult_View, DTO.DevRequestSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LastActionUpdatedDate, o => o.ResolveUsing(s => s.LastActionUpdatedDate.HasValue ? s.LastActionUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EstEndDate, o => o.ResolveUsing(s => s.EstEndDate.HasValue ? s.EstEndDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestMng_DevRequest_View, DTO.DevRequest>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DevRequestAssignments, o => o.MapFrom(s => s.DevRequestMng_DevRequestAssignment_View))
                    .ForMember(d => d.DevRequestHistories, o => o.MapFrom(s => s.DevRequestMng_DevRequestHistory_View))
                    .ForMember(d => d.DevRequestFiles, o => o.MapFrom(s => s.DevRequestMng_DevRequestFile_View))
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EstEndDate, o => o.ResolveUsing(s => s.EstEndDate.HasValue ? s.EstEndDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestMng_DevRequestFile_View, DTO.DevRequestFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestMng_DevRequestHistory_View, DTO.DevRequestHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.DevRequestCommentAttachedFiles, o => o.MapFrom(s => s.DevRequestMng_DevRequestCommentAttachedFile_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestMng_DevRequestCommentAttachedFile_View, DTO.DevRequestCommentAttachedFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestMng_DevRequestAssignment_View, DTO.DevRequestAssignment>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestMng_DevRequestPerson_View, DTO.DevRequestPerson>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestMng_Requester_View, DTO.Requester>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.DevRequest, DevRequest>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DevRequestID, o => o.Ignore())
                    .ForMember(d => d.DevRequestStatusID, o => o.Ignore())
                    .ForMember(d => d.DevRequestAssignment, o => o.Ignore())
                    .ForMember(d => d.DevRequestHistory, o => o.Ignore())
                    .ForMember(d => d.DevRequestFile, o => o.Ignore())
                    .ForMember(d => d.EstEndDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.DevRequestFile, DevRequestFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DevRequestID, o => o.Ignore())
                    .ForMember(d => d.DevRequestFileID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.DevRequestAssignment, DevRequestAssignment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DevRequestID, o => o.Ignore())
                    .ForMember(d => d.DevRequestAssignmentID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.DevRequestHistory, DevRequestHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DevRequestID, o => o.Ignore())
                    .ForMember(d => d.DevRequestHistoryID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.DevRequestCommentAttachedFile, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.DevRequestCommentAttachedFile, DevRequestCommentAttachedFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DevRequestHistoryID, o => o.Ignore())
                    .ForMember(d => d.DevRequestCommentAttachedFileID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DevRequestSearchResult> DB2DTO_DevRequestSearchResult(List<DevRequestMng_DevRequestSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevRequestMng_DevRequestSearchResult_View>, List<DTO.DevRequestSearchResult>>(dbItems);
        }

        public DTO.DevRequest DB2DTO_DevRequest(DevRequestMng_DevRequest_View dbItem)
        {
            return AutoMapper.Mapper.Map<DevRequestMng_DevRequest_View, DTO.DevRequest>(dbItem);
        }

        public List<DTO.DevRequestAssignment> DB2DTO_DevRequestAssignmentList(List<DevRequestMng_DevRequestAssignment_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevRequestMng_DevRequestAssignment_View>, List<DTO.DevRequestAssignment>>(dbItems);
        }

        public List<DTO.DevRequestPerson> DB2DTO_DevRequestPersonList(List<DevRequestMng_DevRequestPerson_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevRequestMng_DevRequestPerson_View>, List<DTO.DevRequestPerson>>(dbItems);
        }

        public List<DTO.Requester> DB2DTO_RequesterList(List<DevRequestMng_Requester_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevRequestMng_Requester_View>, List<DTO.Requester>>(dbItems);
        }

        public void DTO2DB(DTO.DevRequest dtoItem, ref DevRequest dbItem, string TmpFile, int userId)
        {
            // request
            AutoMapper.Mapper.Map<DTO.DevRequest, DevRequest>(dtoItem, dbItem);

            // request file
            foreach (DTO.DevRequestFile dtoFile in dtoItem.DevRequestFiles.Where(o=>o.HasChanged == true))
            {
                // add file
                DevRequestFile dbFile = new DevRequestFile();
                dbItem.DevRequestFile.Add(dbFile);
                AutoMapper.Mapper.Map<DTO.DevRequestFile, DevRequestFile>(dtoFile, dbFile);
                dbFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoFile.NewFile, dtoFile.FileUD, dtoFile.FriendlyName);
            }
        }
    }
}
