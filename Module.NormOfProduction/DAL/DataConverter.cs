using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Module.NormOfProduction.DTO;

namespace Module.NormOfProduction.DAL
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
                //AutoMapper.Mapper.CreateMap<Sample2Mng_SampleOrderSearchResult_View, DTO.SampleOrderSearchResult>()
                //    .IgnoreAllNonExisting()
                //    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                //    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                //    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                //    .ForMember(d => d.Deadline, o => o.ResolveUsing(s => s.Deadline.HasValue ? s.Deadline.Value.ToString("dd/MM/yyyy") : ""))
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<NormOfProductionMng_WorkOrder_View, WorkOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<NormOfProductionMng_BOM_View, BOMDTO>()
                  .IgnoreAllNonExisting()                  
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<NormOfProductionMng_BOMDetail_View, BOMDetailDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<BOMDetailDTO> DB2DTO_BOMDetailDTOs(List<NormOfProductionMng_BOMDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<NormOfProductionMng_BOMDetail_View>, List<BOMDetailDTO>>(dbItems);
        }
        public List<WorkOrderDTO> DB2DTO_WorkOrderDTOs(List<NormOfProductionMng_WorkOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<NormOfProductionMng_WorkOrder_View>, List<WorkOrderDTO>>(dbItems);
        }
        public List<BOMDTO> DB2DTO_BOMDTOs(List<NormOfProductionMng_BOM_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<NormOfProductionMng_BOM_View>, List<BOMDTO>>(dbItems);
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
