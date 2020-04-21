using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.DocumentMonitoringMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DocumentMonitoringMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                
                AutoMapper.Mapper.CreateMap<DocumentMonitoringMng_DocumentMonitoring_SearchView, DTO.DocumentMonitoringMng.DocumentMonitoringSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.VNReceivedDate, o => o.ResolveUsing(s => s.VNReceivedDate.HasValue ? s.VNReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SendToEUDate, o => o.ResolveUsing(s => s.SendToEUDate.HasValue ? s.SendToEUDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA2, o => o.ResolveUsing(s => s.ETA2.HasValue ? s.ETA2.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DocumentMonitoringMng_DocumentMonitoring_View, DTO.DocumentMonitoringMng.DocumentMonitoring>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.VNReceivedDate, o => o.ResolveUsing(s => s.VNReceivedDate.HasValue ? s.VNReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SendToEUDate, o => o.ResolveUsing(s => s.SendToEUDate.HasValue ? s.SendToEUDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA2, o => o.ResolveUsing(s => s.ETA2.HasValue ? s.ETA2.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.ResolveUsing(s => s.ConcurrencyFlag != null ? Convert.ToBase64String(s.ConcurrencyFlag) : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DocumentMonitoringMng.DocumentMonitoring, DocumentMonitoring>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DocumentMonitoringID, o => o.Ignore())
                    .ForMember(d => d.VNReceivedDate, o => o.Ignore())
                    .ForMember(d => d.SendToEUDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<List_DocumentMonitoringRemark_View, DTO.DocumentMonitoringMng.DefaultRemark>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DocumentMonitoringMng.DocumentMonitoringSearchUpdate, DocumentMonitoring>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SendToEUDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public List<DTO.DocumentMonitoringMng.DocumentMonitoringSearch> DB2DTO_DocumentMonitoringSearch(List<DocumentMonitoringMng_DocumentMonitoring_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DocumentMonitoringMng_DocumentMonitoring_SearchView>, List<DTO.DocumentMonitoringMng.DocumentMonitoringSearch>>(dbItems);
        }
        public DTO.DocumentMonitoringMng.DocumentMonitoring DB2DTO_DocumentMonitoring(DocumentMonitoringMng_DocumentMonitoring_View dbItem)
        {
            DTO.DocumentMonitoringMng.DocumentMonitoring dtoItem = AutoMapper.Mapper.Map<DocumentMonitoringMng_DocumentMonitoring_View, DTO.DocumentMonitoringMng.DocumentMonitoring>(dbItem);
            return dtoItem;
        }
        public void DTO2DB_DocumentMonitoring(DTO.DocumentMonitoringMng.DocumentMonitoring dtoItem, ref DocumentMonitoring dbItem)
        {
            AutoMapper.Mapper.Map<DTO.DocumentMonitoringMng.DocumentMonitoring, DocumentMonitoring>(dtoItem, dbItem);
            if (dtoItem.DocumentMonitoringID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
            dbItem.VNReceivedDate = dtoItem.VNReceivedDate.ConvertStringToDateTime();
            dbItem.SendToEUDate = dtoItem.SendToEUDate.ConvertStringToDateTime();
        }

        public List<DTO.DocumentMonitoringMng.DefaultRemark> DB2DTO_DefaultRemark(List<List_DocumentMonitoringRemark_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_DocumentMonitoringRemark_View>, List<DTO.DocumentMonitoringMng.DefaultRemark>>(dbItems);
        }

        public void DTO2DB_QuickDocumentMonitoring(DTO.DocumentMonitoringMng.DocumentMonitoringSearchUpdate dtoItem, ref DocumentMonitoring dbItem)
        {
            AutoMapper.Mapper.Map<DTO.DocumentMonitoringMng.DocumentMonitoringSearchUpdate, DocumentMonitoring>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.SendToEUDate))
            {
                dbItem.SendToEUDate = dtoItem.SendToEUDate.ConvertStringToDateTime();
            }
        }
    }
}
