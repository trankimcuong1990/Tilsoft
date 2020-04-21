using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.MIDeltaByClientOSRpt.DAL
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
                AutoMapper.Mapper.CreateMap<MIDeltaByClientOSRpt_DeltaByClient_View, DTO.DeltaByClientDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_AccountManager_View, DTO.SaleDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MIDeltaByClientOSRpt_AccountManagerSummary_View, DTO.AccountManagerSummaryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DeltaByClientDTO> DB2DTO_DeltaByClient(List<MIDeltaByClientOSRpt_DeltaByClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIDeltaByClientOSRpt_DeltaByClient_View>, List<DTO.DeltaByClientDTO>>(dbItems);
        }

        public List<DTO.SaleDTO> DB2DTO_SaleDTO(List<SupportMng_AccountManager_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_AccountManager_View>, List<DTO.SaleDTO>>(dbItems);
        }

        public List<DTO.AccountManagerSummaryDTO> DB2DTO_AccountManagerSummaryDTO(List<MIDeltaByClientOSRpt_AccountManagerSummary_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIDeltaByClientOSRpt_AccountManagerSummary_View>, List<DTO.AccountManagerSummaryDTO>>(dbItems);
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
