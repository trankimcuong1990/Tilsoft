using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.WarehouseExportMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WarehouseExportMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<WarehouseExportMng_WarehouseExportSearchResult_View, DTO.WarehouseExportMng.WarehouseExportSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ExportedDate, o => o.ResolveUsing(s => s.ExportedDate.HasValue ? s.ExportedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseExportMng_WarehouseExportProductDetail_View, DTO.WarehouseExportMng.WarehouseExportProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseExportMng_WarehouseExport_View, DTO.WarehouseExportMng.WarehouseExport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ExportedDate, o => o.ResolveUsing(s => s.ExportedDate.HasValue ? s.ExportedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Details, o => o.MapFrom(s => s.WarehouseExportMng_WarehouseExportProductDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WarehouseExportMng.WarehouseExportProductDetail, WarehouseExportProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseExportProductDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WarehouseExportMng.WarehouseExport, WarehouseExport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.ProcessingStatusID, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.ExportedDate, o => o.Ignore())
                    .ForMember(d => d.WarehouseExportID, o => o.Ignore())
                    .ForMember(d => d.WarehouseExportProductDetail, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WarehouseExportMng.WarehouseExportSearchResult> DB2DTO_WarehouseExportSearchResultList(List<WarehouseExportMng_WarehouseExportSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseExportMng_WarehouseExportSearchResult_View>, List<DTO.WarehouseExportMng.WarehouseExportSearchResult>>(dbItems);
        }

        public DTO.WarehouseExportMng.WarehouseExport DB2DTO_WarehouseExport(WarehouseExportMng_WarehouseExport_View dbItem)
        {
            return AutoMapper.Mapper.Map<WarehouseExportMng_WarehouseExport_View, DTO.WarehouseExportMng.WarehouseExport>(dbItem);
        }

        public void DTO2DB(DTO.WarehouseExportMng.WarehouseExport dtoItem, ref WarehouseExport dbItem)
        {
            // map main object
            AutoMapper.Mapper.Map<DTO.WarehouseExportMng.WarehouseExport, WarehouseExport>(dtoItem, dbItem);

            // map child collection
            // delete case
            List<int> toBeDeletedDetailIDs = new List<int>();
            foreach (WarehouseExportProductDetail toBeDeletedDetail in dbItem.WarehouseExportProductDetail)
            {
                if (!dtoItem.Details.Select(o => o.WarehouseExportProductDetailID).Contains(toBeDeletedDetail.WarehouseExportProductDetailID))
                {
                    toBeDeletedDetailIDs.Add(toBeDeletedDetail.WarehouseExportProductDetailID);
                }
            }
            foreach (int toBeDeletedDetailID in toBeDeletedDetailIDs)
            {
                dbItem.WarehouseExportProductDetail.Remove(dbItem.WarehouseExportProductDetail.FirstOrDefault(o => o.WarehouseExportProductDetailID == toBeDeletedDetailID));
            }

            foreach (DTO.WarehouseExportMng.WarehouseExportProductDetail dtoDetail in dtoItem.Details)
            {
                // add new case
                WarehouseExportProductDetail dbDetail = null;
                if (dtoDetail.WarehouseExportProductDetailID <= 0)
                {
                    dbDetail = new WarehouseExportProductDetail();
                    dbDetail.WarehouseExport = dbItem;
                    dbItem.WarehouseExportProductDetail.Add(dbDetail);
                }
                else
                {
                    dbDetail = dbItem.WarehouseExportProductDetail.FirstOrDefault(o => o.WarehouseExportProductDetailID == dtoDetail.WarehouseExportProductDetailID);
                }

                if (dbDetail != null)
                {
                    AutoMapper.Mapper.Map<DTO.WarehouseExportMng.WarehouseExportProductDetail, WarehouseExportProductDetail>(dtoDetail, dbDetail);
                }
            }
        }
    }
}
