using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.FactoryBreakdownMng.DAL
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
                AutoMapper.Mapper.CreateMap<FactoryBreakdownMng_FactoryBreakdownSearchResult_View, DTO.FactoryBreakdownSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryBreakdownMng_FactoryBreakdown_View, DTO.FactoryBreakdownDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.FactoryBreakdownDetailDTOs, o => o.MapFrom(s => s.FactoryBreakdownMng_FactoryBreakdownDetail_View))
                    .ForMember(d => d.FactoryBreakdownModels, o => o.MapFrom(s => s.FactoryBreakdownMng_FactoryBreakdownModel_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryBreakdownMng_FactoryBreakdownDetail_View, DTO.FactoryBreakdownDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryBreakdownMng_FactoryBreakdownModel_View, DTO.FactoryBreakdownModelDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryBreakdownDetailDTO, FactoryBreakdownDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryBreakdownDetailID, o => o.Ignore())
                    .ForMember(d => d.FactoryBreakdownCategoryID, o => o.Ignore())
                    .ForMember(d => d.FactoryBreakdownID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryBreakdownDTO, FactoryBreakdown>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryBreakdownID, o => o.Ignore())
                     .ForMember(d => d.UpdatedDate, o => o.Ignore())
                     .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ImportFactoryBreakdown, FactoryBreakdown>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryBreakdownID, o => o.Ignore())
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.ImportFactoryBreakdownDetail, FactoryBreakdownDetail>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryBreakdownDetailID, o => o.Ignore())
                   .ForMember(d => d.FactoryBreakdownCategoryID, o => o.Ignore())
                   .ForMember(d => d.FactoryBreakdownID, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryBreakdownModelDTO, FactoryBreakdownModel>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryBreakdownModelID, o => o.Ignore())
                    .ForMember(d => d.FactoryBreakdownID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryBreakdownSearchResultDTO> DB2DTO_FactoryBreakdownSearchResultDTO(List<FactoryBreakdownMng_FactoryBreakdownSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryBreakdownMng_FactoryBreakdownSearchResult_View>, List<DTO.FactoryBreakdownSearchResultDTO>>(dbItems);
        }

        public DTO.FactoryBreakdownDTO DB2DTO_FactoryBreakdownDTO(FactoryBreakdownMng_FactoryBreakdown_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryBreakdownMng_FactoryBreakdown_View, DTO.FactoryBreakdownDTO>(dbItem);
        }

        public void DTO2DB_FactoryBreakdown(DTO.FactoryBreakdownDTO dtoItem, ref FactoryBreakdown dbItem, int userId)
        {
            // factory breakdown
            dbItem.UpdatedBy = userId;
            dbItem.UpdatedDate = DateTime.Now;

            // remark image
            foreach (FactoryBreakdownDetail dbDetail in dbItem.FactoryBreakdownDetail.ToArray())
            {
                if (!dtoItem.FactoryBreakdownDetailDTOs.Select(o => o.FactoryBreakdownDetailID).Contains(dbDetail.FactoryBreakdownDetailID))
                {
                    dbItem.FactoryBreakdownDetail.Remove(dbDetail);
                }
            }
            foreach (DTO.FactoryBreakdownDetailDTO dtoDetail in dtoItem.FactoryBreakdownDetailDTOs)
            {
                FactoryBreakdownDetail dbDetail;
                if (dtoDetail.FactoryBreakdownDetailID <= 0)
                {
                    dbDetail = new FactoryBreakdownDetail();
                    dbItem.FactoryBreakdownDetail.Add(dbDetail);
                }
                else
                {
                    dbDetail = dbItem.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == dtoDetail.FactoryBreakdownDetailID);
                }

                if (dbDetail != null)
                {
                    // change
                    AutoMapper.Mapper.Map<DTO.FactoryBreakdownDetailDTO, FactoryBreakdownDetail>(dtoDetail, dbDetail);
                }
            }

            // remark image
            foreach (FactoryBreakdownModel dbModel in dbItem.FactoryBreakdownModel.ToArray())
            {
                if (!dtoItem.FactoryBreakdownModels.Select(o => o.FactoryBreakdownModelID).Contains(dbModel.FactoryBreakdownModelID))
                {
                    dbItem.FactoryBreakdownModel.Remove(dbModel);
                }
            }
            foreach (DTO.FactoryBreakdownModelDTO dtoModel in dtoItem.FactoryBreakdownModels)
            {
                FactoryBreakdownModel dbModel;
                if (dtoModel.FactoryBreakdownModelID <= 0)
                {
                    dbModel = new FactoryBreakdownModel();
                    dbItem.FactoryBreakdownModel.Add(dbModel);
                }
                else
                {
                    dbModel = dbItem.FactoryBreakdownModel.FirstOrDefault(o => o.FactoryBreakdownModelID == dtoModel.FactoryBreakdownModelID);
                }

                if (dbModel != null)
                {
                    // change
                    AutoMapper.Mapper.Map<DTO.FactoryBreakdownModelDTO, FactoryBreakdownModel>(dtoModel, dbModel);
                }
            }

            // change
            AutoMapper.Mapper.Map<DTO.FactoryBreakdownDTO, FactoryBreakdown>(dtoItem, dbItem);
        }

        public void DTO2DB_ImportFactoryBreakdown(DTO.ImportFactoryBreakdown dtoItem, ref FactoryBreakdown dbItem, int userId)
        {
            // factory breakdown

            // remark image
            foreach (FactoryBreakdownDetail dbDetail in dbItem.FactoryBreakdownDetail.ToArray())
            {
                if (!dtoItem.FactoryBreakdownDetail.Select(o => o.FactoryBreakdownID).Contains(dbDetail.FactoryBreakdownID) && !dtoItem.FactoryBreakdownDetail.Select(o => o.FactoryBreakdownCategoryID).Contains(dbDetail.FactoryBreakdownCategoryID))
                {
                    dbItem.FactoryBreakdownDetail.Remove(dbDetail);
                }
            }
            foreach (DTO.ImportFactoryBreakdownDetail dtoDetail in dtoItem.FactoryBreakdownDetail)
            {
                FactoryBreakdownDetail dbDetail;

                var factoryBreakdownDetail = dbItem.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownID == dtoDetail.FactoryBreakdownID && o.FactoryBreakdownCategoryID == dtoDetail.FactoryBreakdownCategoryID);

                if (factoryBreakdownDetail == null)
                {
                    dbDetail = new FactoryBreakdownDetail();
                    dbItem.FactoryBreakdownDetail.Add(dbDetail);
                }
                else
                {
                    dbDetail = dbItem.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownID == dtoDetail.FactoryBreakdownID && o.FactoryBreakdownCategoryID == dtoDetail.FactoryBreakdownCategoryID);
                }
                //if (dtoDetail.FactoryBreakdownDetailID <= 0)
                //{
                //    dbDetail = new FactoryBreakdownDetail();
                //    dbItem.FactoryBreakdownDetail.Add(dbDetail);
                //}
                //else
                //{
                //    dbDetail = dbItem.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == dtoDetail.FactoryBreakdownDetailID);
                //}

                if (dbDetail != null)
                {
                    // change
                    dbDetail.Quantity = dtoDetail.Quantity;
                    dbDetail.UnitNM = dtoDetail.UnitNM;
                    //AutoMapper.Mapper.Map<DTO.ImportFactoryBreakdownDetail, FactoryBreakdownDetail>(dtoDetail, dbDetail);
                }
            }
            // factory breakdown
            dbItem.UpdatedBy = userId;
            dbItem.UpdatedDate = DateTime.Now;
            dbItem.IndicatedPrice = dtoItem.IndicatedPrice;
            dbItem.CushionDimensionH = dtoItem.CushionDimensionH;
            dbItem.CushionDimensionL = dtoItem.CushionDimensionL;
            dbItem.CushionDimensionW = dtoItem.CushionDimensionW;
            dbItem.PackingDimensionH = dtoItem.PackingDimensionH;
            dbItem.PackingDimensionL = dtoItem.PackingDimensionL;
            dbItem.PackingDimensionW = dtoItem.PackingDimensionW;
            dbItem.Remark = dtoItem.Remark;
            dbItem.IsConfirmed = true;
            dbItem.ConfirmedBy = userId;
            dbItem.ConfirmedDate = DateTime.Now;

        }
    }
}
