using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Collections;
using AutoMapper;

namespace Module.ProductTestingMng.DAL
{
   public class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate;


        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            //SearchView
            AutoMapper.Mapper.CreateMap<ProductTestingMng_ProductTesting_SearchView, DTO.ProductTestingSearchResultDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.productTestingFileSearchReSultDTOs, o => o.MapFrom(s => s.ProductTestingMng_ProductTesting_SearchFilesView))
                .ForMember(d => d.productTestStandardSearchViewDTOs, o => o.MapFrom(s => s.ProductTestingMng_ProducTestingStandard_SearchView))
                .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //Search Result Files
            AutoMapper.Mapper.CreateMap<ProductTestingMng_ProductTesting_SearchFilesView, DTO.ProductTestingFileSearchReSultDTO>()
               .IgnoreAllNonExisting()
               .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : " ")))
               .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //Serach Result Test Standard
            AutoMapper.Mapper.CreateMap<ProductTestingMng_ProducTestingStandard_SearchView, DTO.ProductTestStandardSearchViewDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //GetView
            AutoMapper.Mapper.CreateMap<ProductTestingMng_ProductTesting_View, DTO.ProductTestingDTO>()
               .IgnoreAllNonExisting()
               .ForMember(d => d.productTestFileDTOs, o => o.MapFrom(s => s.ProductTestingMng_ProductTestFile_View))
               .ForMember(d => d.productTestStandardDTOs, o => o.MapFrom(s => s.ProductTestingMng_ProductTestingStandard_View))
               .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
               .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //Test File
            AutoMapper.Mapper.CreateMap<ProductTestingMng_ProductTestFile_View, DTO.ProductTestFileDTO>()
               .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

            //Test Standard
            AutoMapper.Mapper.CreateMap<ProductTestingMng_ProductTestingStandard_View, DTO.ProductTestStandardDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));


            //Support Product Test Standard list
            AutoMapper.Mapper.CreateMap<SupportMng_ProductTestStandard_View, DTO.SupportProductTestStandard>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO ProductTesting
            AutoMapper.Mapper.CreateMap<DTO.ProductTestingDTO, ProductTest>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductTestID, o => o.Ignore())
                .ForMember(d => d.TestDate, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO Test File
            AutoMapper.Mapper.CreateMap<DTO.ProductTestFileDTO, ProductTestFile>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ProductTestID, o => o.Ignore())
                   .ForMember(d => d.ProductTestFileID, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO Test Standard
            AutoMapper.Mapper.CreateMap<DTO.ProductTestStandardDTO, ProductTestUsingTestStandard>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductTestID, o => o.Ignore())
                .ForMember(d => d.ProductTestUsingTestStandardID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.ProductTestingSearchResultDTO> DB2DTO_SearchResult(List<ProductTestingMng_ProductTesting_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductTestingMng_ProductTesting_SearchView>, List<DTO.ProductTestingSearchResultDTO>>(dbItems);
        }
        public DTO.ProductTestingDTO DB2DTO_ProductTesting(ProductTestingMng_ProductTesting_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductTestingMng_ProductTesting_View, DTO.ProductTestingDTO>(dbItem);
        }

        public void  DTO2DB_ProductTesting(DTO.ProductTestingDTO dtoItem, ref ProductTest dbItem, string TmpFile, int userID)
        {
            //ADD and Remove file
            foreach (ProductTestFile dbFile in dbItem.ProductTestFile.ToArray())
            {
                if (!dtoItem.productTestFileDTOs.Select(o => o.ProductTestFileID).Contains(dbFile.ProductTestFileID))
                {
                    if (!string.IsNullOrEmpty(dbFile.FileUD))
                    {
                        // remove file
                        fwFactory.RemoveImageFile(dbFile.FileUD);
                    }
                    dbItem.ProductTestFile.Remove(dbFile);
                }
            }
            foreach (DTO.ProductTestFileDTO dtoFile in dtoItem.productTestFileDTOs)
            {
                ProductTestFile dbFile;
                if (dtoFile.ProductTestFileID <= 0)
                {
                    dbFile = new ProductTestFile();
                    dbItem.ProductTestFile.Add(dbFile);
                }
                else
                {
                    dbFile = dbItem.ProductTestFile.FirstOrDefault(o => o.ProductTestFileID == dtoFile.ProductTestFileID);
                }

                if (dbFile != null)
                {
                    // change or add file
                    if (dtoFile.ScanHasChange)
                    {
                        dtoFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoFile.ScanNewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                    }
                    AutoMapper.Mapper.Map<DTO.ProductTestFileDTO, ProductTestFile>(dtoFile, dbFile);
                }
            }


            if (dtoItem.productTestFileDTOs != null)
            {
                foreach(var item in dbItem.ProductTestFile.ToArray())
                {
                    if (!dtoItem.productTestFileDTOs.Select(s => s.ProductTestFileID).Contains(item.ProductTestFileID))
                    {
                        dbItem.ProductTestFile.Remove(item);
                    }
                }

                foreach(var item in dtoItem.productTestFileDTOs)
                {
                    ProductTestFile dbProductFile = new ProductTestFile();
                    if(item.ProductTestFileID < 0)
                    {
                        dbItem.ProductTestFile.Add(dbProductFile);
                    }
                    else
                    {
                        dbProductFile = dbItem.ProductTestFile.Where(s => s.ProductTestFileID == item.ProductTestFileID).FirstOrDefault();
                    }
                    if (dbProductFile != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductTestFileDTO, ProductTestFile>(item, dbProductFile);
                    }
                }
            }

            if(dtoItem.productTestStandardDTOs != null)
            {


                foreach(var item in dbItem.ProductTestUsingTestStandard.ToArray())
                {
                    if (!dtoItem.productTestStandardDTOs.Select(o => o.ProductTestUsingTestStandardID).Contains(item.ProductTestUsingTestStandardID))
                    {
                        dbItem.ProductTestUsingTestStandard.Remove(item);
                    }
                }

                foreach(var item in dtoItem.productTestStandardDTOs)
                {
                    ProductTestUsingTestStandard dbTestStandard = new ProductTestUsingTestStandard();
                   if(item.ProductTestUsingTestStandardID < 0)
                    {
                        dbItem.ProductTestUsingTestStandard.Add(dbTestStandard);
                    }
                    else
                    {
                        dbTestStandard = dbItem.ProductTestUsingTestStandard.Where(s => s.ProductTestUsingTestStandardID == item.ProductTestUsingTestStandardID).FirstOrDefault();

                    }
                   if(dbTestStandard != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductTestStandardDTO, ProductTestUsingTestStandard>(item, dbTestStandard);
                    }
                }
            }

             AutoMapper.Mapper.Map<DTO.ProductTestingDTO, ProductTest>(dtoItem, dbItem);

            if (!string.IsNullOrEmpty(dtoItem.TestDate))
            {
                if (DateTime.TryParse(dtoItem.TestDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.TestDate = tmpDate;
                }
            }

            if (!string.IsNullOrEmpty(dtoItem.UpdatedDate))
            {
                if (DateTime.TryParse(dtoItem.UpdatedDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.UpdatedDate = tmpDate;
                }
            }
                //dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
                //dbItem.TestDate = dtoItem.TestDate.ConvertStringToDateTime();
        }

        public List<DTO.SupportProductTestStandard> DB2DTO_spListTestStandard(List<SupportMng_ProductTestStandard_View> spList)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductTestStandard_View>, List<DTO.SupportProductTestStandard>>(spList);
        }
    }
}
