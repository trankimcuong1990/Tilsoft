using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.PlanningProductionMng.DAL
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
                AutoMapper.Mapper.CreateMap<PlanningProductionMng_PlanningProductionSearch_View, DTO.PlanningProductionSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_PlanningProductionTeam_View, DTO.PlanningProductionTeamDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_PlanningProduction_View, DTO.PlanningProductionDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.PlanningProductionDTOs, o => o.MapFrom(s => s.PlanningProductionMng_PlanningProduction_View1))
                   .ForMember(d => d.PlanningProductionTeamDTOs, o => o.MapFrom(s => s.PlanningProductionMng_PlanningProductionTeam_View))
                   .ForMember(d => d.ArisingProductionTeamDTOs, o => o.MapFrom(s => s.PlanningProductionMng_ArisingProductionTeam_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_BOM_ProductionTeam_View, PlanningProductionTeam>()
                  .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_BOM_View, PlanningProduction>()
                    .ForMember(d => d.PlanningProduction1, o => o.MapFrom(s => s.PlanningProductionMng_BOM_View1))
                    .ForMember(d => d.PlanningProductionTeam, o => o.MapFrom(s => s.PlanningProductionMng_BOM_ProductionTeam_View))
                    .ForMember(d => d.PlanningProductionID, o => o.ResolveUsing(s => -1 * s.BOMID))
                    .ForMember(d => d.IsDeleted, o => o.UseValue(false))
                    .ForMember(d => d.ParentPlanningProductionID, o => o.ResolveUsing(s => -1 * s.ParentBOMID));

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_WorkOrder_View, DTO.WorkOrderDTO>()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_WorkCenter_View, DTO.WorkCenterDTO>()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PlanningProductionDTO, PlanningProduction>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.StartDate, o => o.Ignore())
                   .ForMember(d => d.FinishDate, o => o.Ignore())
                   .ForMember(d => d.PlanningProduction1, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_ProductionTeam_View, DTO.ProductionTeamDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PlanningProductionTeamDTO, PlanningProductionTeam>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.PlanningProductionTeamID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_ArisingProductionTeam_View, DTO.ArisingProductionTeamDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_GrantChart_ReceivingDeliveryArisingByDate_View, DTO.GrantChart.ArisingByDate>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_WorkOrder_View, DTO.GrantChart.WorkOrderInfo>()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningProductionMng_GrantChart_ProductionStatisticsDetail_View, DTO.GrantChart.StatisticsByDate>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PlanningProductionSearchDTO> DB2DTO_Search(List<PlanningProductionMng_PlanningProductionSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PlanningProductionMng_PlanningProductionSearch_View>, List<DTO.PlanningProductionSearchDTO>>(dbItems);
        }

        public DTO.PlanningProductionDTO DB2DTO_PlanningProduction(PlanningProductionMng_PlanningProduction_View dbItem)
        {
            return AutoMapper.Mapper.Map<PlanningProductionMng_PlanningProduction_View, DTO.PlanningProductionDTO>(dbItem);
        }

        public void DTO2DB_PlanningProduction(DTO.PlanningProductionDTO dtoItem, ref PlanningProduction dbItem, List<ProductionItem> dbVirualProductionItem)
        {
            PlanningProduction dbPlanningProduction;
            PlanningProduction dbParentPlanningProduction;
            PlanningProductionTeam dbPlanningProductionTeam;
            if (dtoItem.ParentPlanningProductionID.HasValue)
            {
                if (dtoItem.PlanningProductionID < 0)
                {
                    dbPlanningProduction = new PlanningProduction();
                    dbParentPlanningProduction = new PlanningProduction();

                    //mapping node is adding
                    AutoMapper.Mapper.Map<DTO.PlanningProductionDTO, PlanningProduction>(dtoItem, dbPlanningProduction);
                    dbPlanningProduction.StartDate = dtoItem.StartDate.ConvertStringToDateTime();
                    dbPlanningProduction.FinishDate = dtoItem.FinishDate.ConvertStringToDateTime();
                    dbPlanningProduction.ProductionItemID = dbVirualProductionItem.Where(o => o.ProductionItemUD == dtoItem.ProductionItemUD).FirstOrDefault().ProductionItemID;

                    //get parent node and add to parent node
                    GetPlanningProductionByID(dtoItem.ParentPlanningProductionID, dbItem, ref dbParentPlanningProduction);
                    dbParentPlanningProduction.PlanningProduction1.Add(dbPlanningProduction);

                    //mapping production team
                    foreach (var dtoTeamItem in dtoItem.PlanningProductionTeamDTOs)
                    {
                        dbPlanningProductionTeam = new PlanningProductionTeam();
                        dbPlanningProduction.PlanningProductionTeam.Add(dbPlanningProductionTeam);
                        AutoMapper.Mapper.Map<DTO.PlanningProductionTeamDTO, PlanningProductionTeam>(dtoTeamItem, dbPlanningProductionTeam);
                    }
                }
                else
                {
                    dbPlanningProduction = new PlanningProduction();

                    //get node is editing
                    GetPlanningProductionByID(dtoItem.PlanningProductionID, dbItem, ref dbPlanningProduction);

                    //mapping node is editing
                    AutoMapper.Mapper.Map<DTO.PlanningProductionDTO, PlanningProduction>(dtoItem, dbPlanningProduction);
                    dbPlanningProduction.StartDate = dtoItem.StartDate.ConvertStringToDateTime();
                    dbPlanningProduction.FinishDate = dtoItem.FinishDate.ConvertStringToDateTime();

                    //mapping production team
                    foreach (var dbTeamItem in dbPlanningProduction.PlanningProductionTeam.ToArray())
                    {
                        if (!dtoItem.PlanningProductionTeamDTOs.Select(s => s.PlanningProductionTeamID).Contains(dbTeamItem.PlanningProductionTeamID))
                        {
                            dbPlanningProduction.PlanningProductionTeam.Remove(dbTeamItem);
                        }
                    }
                    foreach (var dtoTeamItem in dtoItem.PlanningProductionTeamDTOs)
                    {
                        if (dtoTeamItem.PlanningProductionTeamID > 0)
                        {
                            dbPlanningProductionTeam = dbPlanningProduction.PlanningProductionTeam.Where(o => o.PlanningProductionTeamID == dtoTeamItem.PlanningProductionTeamID).FirstOrDefault();
                        }
                        else {
                            dbPlanningProductionTeam = new PlanningProductionTeam();
                            dbPlanningProduction.PlanningProductionTeam.Add(dbPlanningProductionTeam);
                        }
                        AutoMapper.Mapper.Map<DTO.PlanningProductionTeamDTO, PlanningProductionTeam>(dtoTeamItem, dbPlanningProductionTeam);
                    }
                }
            }
            else
            {
                AutoMapper.Mapper.Map<DTO.PlanningProductionDTO, PlanningProduction>(dtoItem, dbItem);
            }
            foreach (var item in dtoItem.PlanningProductionDTOs)
            {
                DTO2DB_PlanningProduction(item, ref dbItem, dbVirualProductionItem);
            }
        }

        private void GetPlanningProductionByID(int? id, PlanningProduction dbPlanningProduction, ref PlanningProduction dbResult)
        {
            if (dbPlanningProduction.PlanningProductionID == id)
            {
                dbResult = dbPlanningProduction;
            }
            else
            {
                foreach (var item in dbPlanningProduction.PlanningProduction1)
                {
                    GetPlanningProductionByID(id, item, ref dbResult);
                }
            }
        }

        //public List<DTO.SampleOrderSearchResult> DB2DTO_SampleOrderSearchResult(List<Sample2Mng_SampleOrderSearchResult_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<Sample2Mng_SampleOrderSearchResult_View>, List<DTO.SampleOrderSearchResult>>(dbItems);
        //}

        //public DTO.SampleTechnicalDrawing DB2DTO_SampleTechnicalDrawing(Sample2Mng_SampleTechnicalDrawing_View dbItem)
        //{
        //    return AutoMapper.Mapper.Map<Sample2Mng_SampleTechnicalDrawing_View, DTO.SampleTechnicalDrawing>(dbItem);
        //}

        //public void DTO2DB_SampleRemark(DTO.SampleRemark dtoItem, ref SampleRemark dbItem, int userId)
        //{
        //    string TmpFile = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";

        //    // remark
        //    AutoMapper.Mapper.Map<DTO.SampleRemark, SampleRemark>(dtoItem, dbItem);

        //    // remark image
        //    foreach (SampleRemarkImage dbImage in dbItem.SampleRemarkImage.ToArray())
        //    {
        //        if (!dtoItem.SampleRemarkImages.Select(o => o.SampleRemarkImageID).Contains(dbImage.SampleRemarkImageID))
        //        {
        //            if (!string.IsNullOrEmpty(dbImage.FileUD))
        //            {
        //                // remove image file
        //                fwFactory.RemoveImageFile(dbImage.FileUD);
        //            }
        //            dbItem.SampleRemarkImage.Remove(dbImage);
        //        }
        //    }
        //    foreach (DTO.SampleRemarkImage dtoImage in dtoItem.SampleRemarkImages)
        //    {
        //        SampleRemarkImage dbImage;
        //        if (dtoImage.SampleRemarkImageID <= 0)
        //        {
        //            dbImage = new SampleRemarkImage();
        //            dbItem.SampleRemarkImage.Add(dbImage);
        //        }
        //        else
        //        {
        //            dbImage = dbItem.SampleRemarkImage.FirstOrDefault(o => o.SampleRemarkImageID == dtoImage.SampleRemarkImageID);
        //        }

        //        if (dbImage != null)
        //        {
        //            // change or add images
        //            if (dtoImage.HasChanged)
        //            {
        //                dbImage.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoImage.NewFile, dtoImage.FileUD, dtoImage.FriendlyName);
        //            }
        //            AutoMapper.Mapper.Map<DTO.SampleRemarkImage, SampleRemarkImage>(dtoImage, dbImage);
        //        }
        //    }
        //}
    }
}
