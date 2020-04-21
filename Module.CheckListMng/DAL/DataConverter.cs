using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Module.CheckListMng.DTO;

namespace Module.CheckListMng.DAL
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
                AutoMapper.Mapper.CreateMap<CheckListMng_CheckListGroup_View, DTO.CheckListGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Support_TypeOfInspection_View, DTO.TypeOfInspectionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<CheckListMng_CheckList_View, DTO.CheckListDTO>()
                    .IgnoreAllNonExisting()                 
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CheckListDTO, CheckList>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.CheckListDTO> BD2DTO_CheckListMngSearchResult(List<CheckListMng_CheckList_View> items)
        {
            return AutoMapper.Mapper.Map<List<CheckListMng_CheckList_View>, List<DTO.CheckListDTO>>(items);
        }
        public DTO.CheckListDTO DB2DTO_CheckListDTO(CheckListMng_CheckList_View dbItem)
        {
            return AutoMapper.Mapper.Map<CheckListMng_CheckList_View, DTO.CheckListDTO> (dbItem);
        }

        public void DTO2DB_CheckList(DTO.CheckListDTO dto, ref CheckList db)
        {
            AutoMapper.Mapper.Map<DTO.CheckListDTO, CheckList>(dto, db);
        }
        public List<DTO.CheckListGroup> DB2DTO_CheckList(List<CheckListMng_CheckListGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CheckListMng_CheckListGroup_View>, List<DTO.CheckListGroup>>(dbItems);
        }
        public List<DTO.TypeOfInspectionDTO> DB2DTO_Support_TypeOfInspection(List<Support_TypeOfInspection_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Support_TypeOfInspection_View>, List<DTO.TypeOfInspectionDTO>>(dbItems);
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
