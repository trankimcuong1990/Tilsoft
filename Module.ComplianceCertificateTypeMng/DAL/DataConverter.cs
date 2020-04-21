using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.ComplianceCertificateTypeMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ComplianceCertificateTypeMng_ComplianceCertificateType_View, DTO.ComplianceCertificateTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ComplianceCertificateTypeDTO, ComplianceCertificateType>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public DTO.ComplianceCertificateTypeDTO DB2DTO_ComplianceCertificateTypeDTO(ComplianceCertificateTypeMng_ComplianceCertificateType_View dbItem)
        {
            return AutoMapper.Mapper.Map<ComplianceCertificateTypeMng_ComplianceCertificateType_View, DTO.ComplianceCertificateTypeDTO>(dbItem);
        }
        public void DTO2DB_ComplianceCertificateType(DTO.ComplianceCertificateTypeDTO dto, ref ComplianceCertificateType db)
        {
            AutoMapper.Mapper.Map<DTO.ComplianceCertificateTypeDTO, ComplianceCertificateType>(dto, db);
        }
        public List<DTO.ComplianceCertificateTypeDTO> DB2DTO_ComplianceCertificateTypeSearch(List<ComplianceCertificateTypeMng_ComplianceCertificateType_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ComplianceCertificateTypeMng_ComplianceCertificateType_View>, List<DTO.ComplianceCertificateTypeDTO>>(dbItem);
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
