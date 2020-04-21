using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.QCReport.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;
        public DataConverter()
        {
            string mapName = "QCReportMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<QCReportMng_FactoryOrderDetail_View, DTO.FactoryOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_FactoryOrderDetailSearchResult_View, DTO.FactoryOrderDetailSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReport_View, DTO.QCReport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.InspectedDay, o => o.ResolveUsing(s => s.InspectedDay.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.QCReportImages, o => o.MapFrom(s => s.QCReportMng_QCReportImage_View))
                    .ForMember(d => d.QCReportDetails, o => o.MapFrom(s => s.QCReportMng_QCReportDetail_View))
                    .ForMember(d => d.QCReportDefects, o => o.MapFrom(s => s.QCReportMng_QCReportDefect_View))
                    .ForMember(d => d.QCReportDocuments, o => o.MapFrom(s => s.QCReportMng_QCReportDocument_View)) // qc report document files | tran.cuong | 20180228
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportImage_View, DTO.QCReportImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportDetail_View, DTO.QCReportDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportDefect_View, DTO.QCReportDefect>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportSearchResult_View, DTO.QCReportSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QCReport, QCReport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.InspectedDay, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.QCReportID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.QCReportImage, QCReportImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QCReportImageID, o => o.Ignore())
                    .ForMember(d => d.QCReportID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.QCReportDocument, QCReportDocument>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QCReportDocumentID, o => o.Ignore())
                    .ForMember(d => d.QCReportID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.QCReportDetail, QCReportDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QCReportDetailID, o => o.Ignore())
                    .ForMember(d => d.QCReportID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.QCReportDefect, QCReportDefect>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QCReportDefectID, o => o.Ignore())
                    .ForMember(d => d.QCReportID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<QCReportImageTitle, DTO.QCReportImageTitleDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // qc report document files | tran.cuong | 20180227 | start
                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportDocument_View, DTO.QCReportDocument>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")));
                // qc report document files | tran.cuong | 20180227 | e n d

                // qc report document type | tran.cuong | 20180228 | start
                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportDocumentType_View, DTO.QCReportDocumentType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // qc report document type | tran.cuong | 20180228 | e n d

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.QCReportSearchResult> DB2DTO_QCReportSearchResultList(List<QCReportMng_QCReportSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_QCReportSearchResult_View>, List<DTO.QCReportSearchResult>>(dbItems);
        }

        public List<DTO.FactoryOrderDetailSearchResult> DB2DTO_FactoryOrderDetailSearchResultList(List<QCReportMng_FactoryOrderDetailSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_FactoryOrderDetailSearchResult_View>, List<DTO.FactoryOrderDetailSearchResult>>(dbItems);
        }

        public DTO.FactoryOrderDetail DB2DTO_FactoryOrderDetail(QCReportMng_FactoryOrderDetail_View dbItems)
        {
            return AutoMapper.Mapper.Map<QCReportMng_FactoryOrderDetail_View, DTO.FactoryOrderDetail>(dbItems);
        }

        public DTO.QCReport DB2DTO_QCReport(QCReportMng_QCReport_View dbItem)
        {
            return AutoMapper.Mapper.Map<QCReportMng_QCReport_View, DTO.QCReport>(dbItem);
        }

        public List<DTO.QCReport> DB2DTO_ListQCReport(List<QCReportMng_QCReport_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_QCReport_View>, List<DTO.QCReport>>(dbItem);
        }
        public List<DTO.QCReportImageTitleDTO> DB2DTO_QCReportImageTitles(List<QCReportImageTitle> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QCReportImageTitle>, List<DTO.QCReportImageTitleDTO>>(dbItems);
        }
        public void DTO2DB(DTO.QCReport dtoItem, ref QCReport dbItem, string _tempFolder, int userId)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.QCReport, QCReport>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.InspectedDay = dtoItem.InspectedDay.ConvertStringToDateTime();
            if (dtoItem.AttachedFile_HasChanged)
            {
                dbItem.FileUD = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.AttachedFile_NewFile, dtoItem.FileUD);
            }

            // map qc report image
            if (dtoItem.QCReportImages != null)
            {
                // check for child rows deleted
                foreach (QCReportImage dbDraw in dbItem.QCReportImage.ToArray())
                {
                    if (!dtoItem.QCReportImages.Select(o => o.QCReportImageID).Contains(dbDraw.QCReportImageID))
                    {
                        dbItem.QCReportImage.Remove(dbDraw);
                    }
                }

                // map child rows
                foreach (DTO.QCReportImage dtoImage in dtoItem.QCReportImages)
                {
                    QCReportImage dbImage;
                    if (dtoImage.QCReportImageID <= 0)
                    {
                        dbImage = new QCReportImage();
                        dbItem.QCReportImage.Add(dbImage);
                        dtoItem.FileUD = dtoImage.FileUD;
                    }
                    else
                    {
                        dbImage = dbItem.QCReportImage.FirstOrDefault(o => o.QCReportImageID == dtoImage.QCReportImageID);
                    }
                    //dtoItem.dbImage.Last();
                    //dbImage.UpdatedDate = DateTime.Now;
                    if (dbImage != null)
                    {
                        AutoMapper.Mapper.Map<DTO.QCReportImage, QCReportImage>(dtoImage, dbImage);
                    }
                }
                if (dtoItem.QCReportImages.Count > 0)
                {
                    var item = dtoItem.QCReportImages.Count - 1;
                    dtoItem.FileUD = dtoItem.QCReportImages[item].FileUD;
                }
            }

            // map qc report document
            if (dtoItem.QCReportDocuments != null)
            {
                // check for child rows deleted
                foreach (QCReportDocument dbDocument in dbItem.QCReportDocument.ToArray())
                {
                    if (!dtoItem.QCReportDocuments.Select(o => o.QCReportDocumentID).Contains(dbDocument.QCReportDocumentID))
                    {
                        dbItem.QCReportDocument.Remove(dbDocument);
                    }
                }

                // map child rows
                foreach (DTO.QCReportDocument dtoDocument in dtoItem.QCReportDocuments)
                {
                    QCReportDocument dbDocument;
                    if (dtoDocument.QCReportDocumentID <= 0)
                    {
                        dbDocument = new QCReportDocument();
                        dbItem.QCReportDocument.Add(dbDocument);
                        dtoItem.FileUD = dtoDocument.FileUD;
                    }
                    else
                    {
                        dbDocument = dbItem.QCReportDocument.FirstOrDefault(o => o.QCReportDocumentID == dtoDocument.QCReportDocumentID);
                    }
                    
                    if (dbDocument != null)
                    {
                        AutoMapper.Mapper.Map<DTO.QCReportDocument, QCReportDocument>(dtoDocument, dbDocument);

                        dbDocument.UpdatedBy = userId;
                        dbDocument.UpdatedDate = DateTime.Now;
                    }
                }
                if (dtoItem.QCReportDocuments.Count > 0)
                {
                    var item = dtoItem.QCReportDocuments.Count - 1;
                    dtoItem.FileUD = dtoItem.QCReportDocuments[item].FileUD;
                }
            }

            // map qc detail
            if (dtoItem.QCReportDetails != null)
            {
                //check for child rows deleted
                foreach (QCReportDetail dbDetail in dbItem.QCReportDetail.ToArray())
                {
                    if (!dtoItem.QCReportDetails.Select(o => o.QCReportDetailID).Contains(dbDetail.QCReportDetailID))
                    {
                        dbItem.QCReportDetail.Remove(dbDetail);
                    }
                }

                //map child row
                foreach (DTO.QCReportDetail dtoDetail in dtoItem.QCReportDetails)
                {
                    QCReportDetail dbDetail;
                    if (dtoDetail.QCReportDetailID <= 0)
                    {
                        dbDetail = new QCReportDetail();
                        dbItem.QCReportDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.QCReportDetail.FirstOrDefault(o => o.QCReportDetailID == dtoDetail.QCReportDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.QCReportDetail, QCReportDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            // map qc defect
            if (dtoItem.QCReportDefects != null)
            {
                //check for child rows deleted
                foreach (QCReportDefect dbDefect in dbItem.QCReportDefect.ToArray())
                {
                    if (!dtoItem.QCReportDefects.Select(o => o.QCReportDefectID).Contains(dbDefect.QCReportDefectID))
                    {
                        dbItem.QCReportDefect.Remove(dbDefect);
                    }
                }

                //map child row
                foreach (DTO.QCReportDefect dtoDefect in dtoItem.QCReportDefects)
                {
                    QCReportDefect dbDefect;
                    if (dtoDefect.QCReportDefectID <= 0)
                    {
                        dbDefect = new QCReportDefect();
                        dbItem.QCReportDefect.Add(dbDefect);
                    }
                    else
                    {
                        dbDefect = dbItem.QCReportDefect.FirstOrDefault(o => o.QCReportDefectID == dtoDefect.QCReportDefectID);
                    }

                    if (dbDefect != null)
                    {
                        AutoMapper.Mapper.Map<DTO.QCReportDefect, QCReportDefect>(dtoDefect, dbDefect);
                    }
                }
            }
        }

        public List<DTO.QCReportDocumentType> DB2DTO_QCReportDocumentType(List<QCReportMng_QCReportDocumentType_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_QCReportDocumentType_View>, List<DTO.QCReportDocumentType>>(dbItem);
        }
    }
}
