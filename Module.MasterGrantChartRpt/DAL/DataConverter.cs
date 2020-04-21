using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.MasterGrantChartRpt.DAL
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
                AutoMapper.Mapper.CreateMap<MasterGrantChartRpt_WorkOrder_View, DTO.WorkOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WeekStart, o => o.ResolveUsing(s => s.WeekStart.HasValue ? s.WeekStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WeekEnd, o => o.ResolveUsing(s => s.WeekEnd.HasValue ? s.WeekEnd.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WeekInfo, DTO.WeekInfoDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WeekStart, o => o.ResolveUsing(s => s.WeekStart.HasValue ? s.WeekStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WeekEnd, o => o.ResolveUsing(s => s.WeekEnd.HasValue ? s.WeekEnd.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkCenter, DTO.WorkCenterDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MasterGrantChartRpt_WorkOrderProductionDetail_View, DTO.WorkOrderProductionDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WorkOrderDTO> DB2DTO_WorkOrderList(List<MasterGrantChartRpt_WorkOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MasterGrantChartRpt_WorkOrder_View>, List<DTO.WorkOrderDTO>>(dbItems);
        }

        public List<DTO.WeekInfoDTO> DB2DTO_WeekInfoList(List<WeekInfo> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WeekInfo>, List<DTO.WeekInfoDTO>>(dbItems);
        }

        public List<DTO.WorkCenterDTO> DB2DTO_WorkCenterList(List<WorkCenter> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkCenter>, List<DTO.WorkCenterDTO>>(dbItems);
        }

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
