using AutoMapper;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt.DAL
{
    internal class DataConverter
    {
        private readonly Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = "QualityInspectionRpt";

            if (FrameworkSetting.Setting.Maps.Contains(mapName)) { return; }

            Mapper.CreateMap<QualityInspectionRpt_QualityInspectionType_View, DTO.QualityInspectionTypeData>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<DTO.QualityInspectionTypeData, QualityInspectionType>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityInspectionTypeID, o => o.Ignore());

            Mapper.CreateMap<QualityInspectionRpt_QualityInspectionCorrectAction_View, DTO.QualityInspectionCorrectActionData>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<DTO.QualityInspectionCorrectActionData, QualityInspectionCorrectAction>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityInspectionCorrectActionID, o => o.Ignore());

            Mapper.CreateMap<QualityInspectionRpt_QualityInspectionDefaultSetting_View, DTO.QualityInspectionDefaultSettingData>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<DTO.QualityInspectionDefaultSettingData, QualityInspectionDefaultSetting>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityInspectionDefaultSettingID, o => o.Ignore());

            Mapper.CreateMap<QualityInspectionRpt_SupportWickerColor_View, DTO.SupportWickerColorData>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<QualityInspectionRpt_QualityInspectionDefaultSetting_View, DTO.QualityInspectionCategoryData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityInspectionCategoryID, o => o.UseValue(-1))
                .ForMember(d => d.Description, o => o.ResolveUsing(s => s.Description));

            Mapper.CreateMap<QualityInspectionRpt_InitQualityInspection_View, DTO.QualityInspectionData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.WorkOrderID, o => o.ResolveUsing(s => s.WorkOrderID))
                .ForMember(d => d.WorkOrderUD, o => o.ResolveUsing(s => s.WorkOrderUD))
                .ForMember(d => d.ClientID, o => o.ResolveUsing(s => s.ClientID))
                .ForMember(d => d.ClientUD, o => o.ResolveUsing(s => s.ClientUD))
                .ForMember(d => d.ProductionItemID, o => o.ResolveUsing(s => s.ProductionItemID))
                .ForMember(d => d.ProductionItemUD, o => o.ResolveUsing(s => s.ProductionItemUD))
                .ForMember(d => d.ProductionItemNM, o => o.ResolveUsing(s => s.ProductionItemNM));

            Mapper.CreateMap<DTO.QualityInspectionData, QualityInspection>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityInspectionID, o => o.Ignore())
                .ForMember(d => d.WorkCenterWorkingDate, o => o.Ignore())
                .ForMember(d => d.InspectionDate, o => o.Ignore())
                .ForMember(d => d.ReceivedDate, o => o.Ignore())
                .ForMember(d => d.ApprovedDate, o => o.Ignore())
                .ForMember(d => d.QualityInspectionCategory, o => o.Ignore());

            Mapper.CreateMap<DTO.QualityInspectionCategoryData, QualityInspectionCategory>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityInspectionCategoryID, o => o.Ignore())
                .ForMember(d => d.QualityInspectionCategoryImage, o => o.Ignore());

            Mapper.CreateMap<DTO.QualityInspectionCategoryImageData, QualityInspectionCategoryImage>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityInspectionCategoryImageID, o => o.Ignore());

            Mapper.CreateMap<QualityInspectionRpt_QualityInspectionSearchResult_View, DTO.QualityInspectionSearchResultData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.InspectionDate, o => o.ResolveUsing(s => s.InspectionDate.HasValue ? s.InspectionDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.ReceivedDate, o => o.ResolveUsing(s => s.ReceivedDate.HasValue ? s.ReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.WorkCenterWorkingDate, o => o.ResolveUsing(s => s.WorkCenterWorkingDate.HasValue ? s.WorkCenterWorkingDate.Value.ToString("dd/MM/yyyy") : ""));

            Mapper.CreateMap<QualityInspectionRpt_QualityInspection_View, DTO.QualityInspectionData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.InspectionDate, o => o.ResolveUsing(s => s.InspectionDate.HasValue ? s.InspectionDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.ReceivedDate, o => o.ResolveUsing(s => s.ReceivedDate.HasValue ? s.ReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.WorkCenterWorkingDate, o => o.ResolveUsing(s => s.WorkCenterWorkingDate.HasValue ? s.WorkCenterWorkingDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.QualityInspectionCategories, o => o.MapFrom(s => s.QualityInspectionRpt_QualityInspectionCategory_View));

            Mapper.CreateMap<QualityInspectionRpt_QualityInspectionCategory_View, DTO.QualityInspectionCategoryData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityInpsectionCategoryImages, o => o.MapFrom(s => s.QualityInspectionRpt_QualityInspectionCategoryImage_View));

            Mapper.CreateMap<QualityInspectionRpt_QualityInspectionCategoryImage_View, DTO.QualityInspectionCategoryImageData>()
                .IgnoreAllNonExisting();
        }

        public List<DTO.QualityInspectionDefaultSettingData> DB2DTO_QualityInspectionDefaultSettings(List<QualityInspectionRpt_QualityInspectionDefaultSetting_View> dbItem)
        {
            return Mapper.Map<List<QualityInspectionRpt_QualityInspectionDefaultSetting_View>, List<DTO.QualityInspectionDefaultSettingData>>(dbItem);
        }

        public void DTO2DB_QualityInspectionDefaultSetting(DTO.QualityInspectionDefaultSettingData dtoItem, ref QualityInspectionDefaultSetting dbItem)
        {
            Mapper.Map<DTO.QualityInspectionDefaultSettingData, QualityInspectionDefaultSetting>(dtoItem, dbItem);
        }

        public List<DTO.QualityInspectionTypeData> DB2DTO_QualityInspectionTypes(List<QualityInspectionRpt_QualityInspectionType_View> dbItem)
        {
            return Mapper.Map<List<QualityInspectionRpt_QualityInspectionType_View>, List<DTO.QualityInspectionTypeData>>(dbItem);
        }

        public void DTO2DB_QualityInspectionType(DTO.QualityInspectionTypeData dtoItem, ref QualityInspectionType dbItem)
        {
            Mapper.Map<DTO.QualityInspectionTypeData, QualityInspectionType>(dtoItem, dbItem);
        }

        public List<DTO.QualityInspectionCorrectActionData> DB2DTO_QualityInspectionCorrectActions(List<QualityInspectionRpt_QualityInspectionCorrectAction_View> dbItem)
        {
            return Mapper.Map<List<QualityInspectionRpt_QualityInspectionCorrectAction_View>, List<DTO.QualityInspectionCorrectActionData>>(dbItem);
        }

        public void DTO2DB_QualityInspectionCorrectAction(DTO.QualityInspectionCorrectActionData dtoItem, ref QualityInspectionCorrectAction dbItem)
        {
            Mapper.Map<DTO.QualityInspectionCorrectActionData, QualityInspectionCorrectAction>(dtoItem, dbItem);
        }

        public List<DTO.SupportWickerColorData> DB2DTO_SupportWickerColor(List<QualityInspectionRpt_SupportWickerColor_View> dbItem)
        {
            return Mapper.Map<List<QualityInspectionRpt_SupportWickerColor_View>, List<DTO.SupportWickerColorData>>(dbItem);
        }

        public List<DTO.QualityInspectionCategoryData> DB2DTO_InitQualityInspectionCategories(List<QualityInspectionRpt_QualityInspectionDefaultSetting_View> dbItem)
        {
            return Mapper.Map<List<QualityInspectionRpt_QualityInspectionDefaultSetting_View>, List<DTO.QualityInspectionCategoryData>>(dbItem);
        }

        public DTO.QualityInspectionData DB2DTO_InitQualityInspection(QualityInspectionRpt_InitQualityInspection_View dbItem)
        {
            return Mapper.Map<QualityInspectionRpt_InitQualityInspection_View, DTO.QualityInspectionData>(dbItem);
        }

        public void DTO2DB_QualityInspection(DTO.QualityInspectionData dtoItem, ref QualityInspection dbItem, string templateFile, int userID)
        {
            Mapper.Map<DTO.QualityInspectionData, QualityInspection>(dtoItem, dbItem);

            dbItem.WorkCenterWorkingDate = dtoItem.WorkCenterWorkingDate.ConvertStringToDateTime();
            dbItem.InspectionDate = dtoItem.InspectionDate.ConvertStringToDateTime();
            dbItem.ReceivedDate = dtoItem.ReceivedDate.ConvertStringToDateTime();

            foreach (var dbItemCategory in dbItem.QualityInspectionCategory.ToArray())
            {
                if (!dtoItem.QualityInspectionCategories.Select(o => o.QualityInspectionCategoryID).Contains(dbItemCategory.QualityInspectionCategoryID))
                {
                    foreach (var dbItemCategoryImage in dbItemCategory.QualityInspectionCategoryImage.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbItemCategoryImage.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbItemCategoryImage.FileUD);
                        }

                        dbItemCategory.QualityInspectionCategoryImage.Remove(dbItemCategoryImage);
                    }

                    dbItem.QualityInspectionCategory.Remove(dbItemCategory);
                }
            }

            foreach (var dtoItemCategory in dtoItem.QualityInspectionCategories)
            {
                QualityInspectionCategory dbItemCategory;

                if (dtoItemCategory.QualityInspectionCategoryID < 0)
                {
                    dbItemCategory = new QualityInspectionCategory();
                    dbItem.QualityInspectionCategory.Add(dbItemCategory);
                }
                else
                {
                    dbItemCategory = dbItem.QualityInspectionCategory.FirstOrDefault(o => o.QualityInspectionCategoryID == dtoItemCategory.QualityInspectionCategoryID);
                }

                if (dbItemCategory != null)
                {
                    Mapper.Map<DTO.QualityInspectionCategoryData, QualityInspectionCategory>(dtoItemCategory, dbItemCategory);

                    foreach (var dbItemCategoryImage in dbItemCategory.QualityInspectionCategoryImage.ToArray())
                    {
                        if (!dtoItemCategory.QualityInpsectionCategoryImages.Select(s => s.QualityInspectionCategoryImageID).Contains(dbItemCategoryImage.QualityInspectionCategoryImageID))
                        {
                            if (!string.IsNullOrEmpty(dbItemCategoryImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbItemCategoryImage.FileUD);
                            }

                            dbItemCategory.QualityInspectionCategoryImage.Remove(dbItemCategoryImage);
                        }
                    }

                    foreach (var dtoItemCategoryImage in dtoItemCategory.QualityInpsectionCategoryImages)
                    {
                        QualityInspectionCategoryImage dbItemCategoryImage;

                        if (dtoItemCategoryImage.QualityInspectionCategoryImageID < 0)
                        {
                            dbItemCategoryImage = new QualityInspectionCategoryImage();
                            dbItemCategory.QualityInspectionCategoryImage.Add(dbItemCategoryImage);
                        }
                        else
                        {
                            dbItemCategoryImage = dbItemCategory.QualityInspectionCategoryImage.FirstOrDefault(o => o.QualityInspectionCategoryImageID == dtoItemCategoryImage.QualityInspectionCategoryImageID);
                        }

                        if (dbItemCategoryImage != null)
                        {
                            Mapper.Map<DTO.QualityInspectionCategoryImageData, QualityInspectionCategoryImage>(dtoItemCategoryImage, dbItemCategoryImage);

                            if (dtoItemCategoryImage.HasChange.HasValue && dtoItemCategoryImage.HasChange.Value)
                            {
                                dbItemCategoryImage.FileUD = fwFactory.CreateFilePointer(templateFile, dtoItemCategoryImage.NewFile, dtoItemCategoryImage.FileUD, dtoItemCategoryImage.FriendlyName);
                            }
                        }
                    }
                }
            }
        }

        public List<DTO.QualityInspectionSearchResultData> DB2DTO_QualityInspections(List<QualityInspectionRpt_QualityInspectionSearchResult_View> dbItem)
        {
            return Mapper.Map<List<QualityInspectionRpt_QualityInspectionSearchResult_View>, List<DTO.QualityInspectionSearchResultData>>(dbItem);
        }

        public DTO.QualityInspectionData DB2DTO_QualityInspection(QualityInspectionRpt_QualityInspection_View dbItem)
        {
            return Mapper.Map<QualityInspectionRpt_QualityInspection_View, DTO.QualityInspectionData>(dbItem);
        }
    }
}
