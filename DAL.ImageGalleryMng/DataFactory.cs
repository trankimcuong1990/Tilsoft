using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ImageGalleryMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ImageGalleryMng.SearchFormData, DTO.ImageGalleryMng.EditFormData, DTO.ImageGalleryMng.ImageGallery>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private string _TempFolder;

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private ImageGalleryMngEntities CreateContext()
        {
            return new ImageGalleryMngEntities(DALBase.Helper.CreateEntityConnectionString("ImageGalleryMngModel"));
        }

        public override DTO.ImageGalleryMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ImageGalleryMng.SearchFormData data = new DTO.ImageGalleryMng.SearchFormData();
            data.Data = new List<DTO.ImageGalleryMng.ImageGallerySearchResult>();
            totalRows = 0;

            string ModelNM = null;            
            string ClientUD = null;
            string MaterialNM = null;
            string MaterialTypeNM = null;
            string MaterialColorNM = null;
            string BackCushionNM = null;
            string SeatCushionNM = null;
            string CushionColorNM = null;
            string SampleImportBy = null;
            int? SeasonTypeID = null;
            int? GalleryItemTypeID = null;
            bool? IsFinalized = null;
            if (filters.ContainsKey("ModelNM") && !string.IsNullOrEmpty(filters["ModelNM"].ToString()))
            {
                ModelNM = filters["ModelNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("MaterialNM") && !string.IsNullOrEmpty(filters["MaterialNM"].ToString()))
            {
                MaterialNM = filters["MaterialNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("MaterialTypeNM") && !string.IsNullOrEmpty(filters["MaterialTypeNM"].ToString()))
            {
                MaterialTypeNM = filters["MaterialTypeNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("MaterialColorNM") && !string.IsNullOrEmpty(filters["MaterialColorNM"].ToString()))
            {
                MaterialColorNM = filters["MaterialColorNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("BackCushionNM") && !string.IsNullOrEmpty(filters["BackCushionNM"].ToString()))
            {
                BackCushionNM = filters["BackCushionNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SeatCushionNM") && !string.IsNullOrEmpty(filters["SeatCushionNM"].ToString()))
            {
                SeatCushionNM = filters["SeatCushionNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("CushionColorNM") && !string.IsNullOrEmpty(filters["CushionColorNM"].ToString()))
            {
                CushionColorNM = filters["CushionColorNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SampleImportBy") && !string.IsNullOrEmpty(filters["SampleImportBy"].ToString()))
            {
                SampleImportBy = filters["SampleImportBy"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SeasonTypeID") && filters["SeasonTypeID"] != null && !string.IsNullOrEmpty(filters["SeasonTypeID"].ToString()))
            {
                SeasonTypeID = Convert.ToInt32(filters["SeasonTypeID"]);
            }
            if (filters.ContainsKey("GalleryItemTypeID") && filters["GalleryItemTypeID"] != null && !string.IsNullOrEmpty(filters["GalleryItemTypeID"].ToString()))
            {
                GalleryItemTypeID = Convert.ToInt32(filters["GalleryItemTypeID"]);
            }
            if (filters.ContainsKey("IsFinalized") && filters["IsFinalized"] != null && !string.IsNullOrEmpty(filters["IsFinalized"].ToString()))
            {
                IsFinalized = (filters["IsFinalized"].ToString() == "true") ? true : false;
            }

            //try to get data
            try
            {
                using (ImageGalleryMngEntities context = CreateContext())
                {
                    totalRows = context.ImageGalleryMng_function_SearchImageGallery(ModelNM, GalleryItemTypeID, SeasonTypeID, ClientUD, MaterialNM, MaterialTypeNM, MaterialColorNM, BackCushionNM, SeatCushionNM, CushionColorNM, SampleImportBy, IsFinalized, orderBy, orderDirection).Count();
                    var result = context.ImageGalleryMng_function_SearchImageGallery(ModelNM, GalleryItemTypeID, SeasonTypeID, ClientUD, MaterialNM, MaterialTypeNM, MaterialColorNM, BackCushionNM, SeatCushionNM, CushionColorNM, SampleImportBy, IsFinalized, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ImageGallerySearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.ImageGalleryMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ImageGalleryMng.EditFormData data = new DTO.ImageGalleryMng.EditFormData();
            data.Data = new DTO.ImageGalleryMng.ImageGallery();
            data.Data.ImageGalleryClients = new List<DTO.ImageGalleryMng.ImageGalleryClient>();
            data.Data.ImageGalleryVersions = new List<DTO.ImageGalleryMng.ImageGalleryVersion>();

            data.Materials = new List<DTO.Support.Material>();
            data.MaterialTypes = new List<DTO.Support.MaterialTypeOptionRaw>();
            data.MaterialColors = new List<DTO.Support.MaterialColorOptionRaw>();
            data.BackCushionOptions = new List<DTO.Support.BackCushionOptionRaw>();
            data.SeatCushionOption = new List<DTO.Support.SeatCushionOptionRaw>();
            data.CushionColors = new List<DTO.Support.CushionColorOptionRaw>();
            data.SeasonTypes = new List<DTO.Support.SeasonType>();
            data.Models = new List<DTO.Support.Model>();
            data.GalleryItemTypes = new List<DTO.Support.GalleryItemType>();
            data.Clients = new List<DTO.Support.Client>();

            //try to get data
            try
            {
                using (ImageGalleryMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_ImageGallery(context.ImageGalleryMng_ImageGallery_View
                            .Include("ImageGalleryMng_ImageGalleryClient_View")
                            .Include("ImageGalleryMng_ImageGalleryVersion_View")
                            .FirstOrDefault(o => o.ImageGalleryID == id));
                    }

                    data.Materials = supportFactory.GetMaterial().ToList();
                    data.MaterialTypes = supportFactory.GetMaterialTypeOptionRaw().ToList();
                    data.MaterialColors = supportFactory.GetMaterialColorOptionRaw().ToList();
                    data.BackCushionOptions = supportFactory.GetBackCushionOptionRaw().ToList();
                    data.SeatCushionOption = supportFactory.GetSeatCushionOptionRaw().ToList();
                    data.CushionColors = supportFactory.GetCushionColorOptionRaw().ToList();
                    data.SeasonTypes = supportFactory.GetSeasonType().ToList();
                    data.Models = supportFactory.GetModel().ToList();
                    data.GalleryItemTypes = supportFactory.GetGalleryItemType().ToList();
                    data.Clients = supportFactory.GetClient().ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ImageGalleryMngEntities context = CreateContext())
                {
                    ImageGallery dbItem = context.ImageGallery.FirstOrDefault(o => o.ImageGalleryID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Item not found!";
                        return false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dbItem.FileUD))
                        {
                            // remove image
                            fwFactory.RemoveImageFile(dbItem.FileUD);
                        }

                        //dbItem.ImageGalleryClient
                        foreach (ImageGalleryVersion version in dbItem.ImageGalleryVersion.ToArray())
                        {
                            if (!string.IsNullOrEmpty(version.FileUD))
                            {
                                // remove image
                                fwFactory.RemoveImageFile(version.FileUD);
                            }
                            context.ImageGalleryVersion.Remove(version);
                        }
                        foreach (ImageGalleryClient client in dbItem.ImageGalleryClient.ToArray())
                        {
                            context.ImageGalleryClient.Remove(client);
                        }

                        context.ImageGallery.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.ImageGalleryMng.ImageGallery dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ImageGalleryMngEntities context = CreateContext())
                {
                    ImageGallery dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ImageGallery();
                        context.ImageGallery.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ImageGallery.FirstOrDefault(o => o.ImageGalleryID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Item not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // process image
                        foreach (DTO.ImageGalleryMng.ImageGalleryVersion dtoVersion in dtoItem.ImageGalleryVersions.Where(o => o.HasChange))
                        {
                            //dbItem.FileUD = (new Framework.DataFactory()).CreateFilePointer(this._TempFolder, dtoItem.NewFile, dtoItem.FileUD);
                            string[] videoExtension = { "avi", "mp4", "wmv", "flv" };
                            if (videoExtension.Contains(dtoVersion.NewFile.Substring(dtoVersion.NewFile.Length - 3, 3).ToLower()))
                            {
                                dtoVersion.FileUD = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoVersion.NewFile, dtoVersion.FileUD);
                            }
                            else
                            {
                                dtoVersion.FileUD = fwFactory.CreateFilePointer(this._TempFolder, dtoVersion.NewFile, dtoVersion.FileUD);
                            }
                        }
                        converter.DTO2DB(dtoItem, ref dbItem);

                        // remove orphan
                        context.ImageGalleryClient.Local.Where(o => o.ImageGallery == null).ToList().ForEach(o => context.ImageGalleryClient.Remove(o));
                        foreach (ImageGalleryVersion dbVersion in context.ImageGalleryVersion.Local.Where(o => o.ImageGallery == null).ToList())
                        {
                            // remove images
                            if (!string.IsNullOrEmpty(dbVersion.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbVersion.FileUD);
                            }
                        }
                        context.ImageGalleryVersion.Local.Where(o => o.ImageGallery == null).ToList().ForEach(o => context.ImageGalleryVersion.Remove(o));

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ImageGalleryID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.ImageGalleryMng.ImageGallery dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ImageGalleryMng.ImageGallery dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.ImageGalleryMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ImageGalleryMng.SearchFilterData data = new DTO.ImageGalleryMng.SearchFilterData();
            data.SeasonTypes = new List<DTO.Support.SeasonType>();
            data.YesNos = new List<DTO.Support.YesNo>();
            data.GalleryItemTypes = new List<DTO.Support.GalleryItemType>();

            //try to get data
            try
            {
                data.SeasonTypes = supportFactory.GetSeasonType().ToList();
                data.YesNos = supportFactory.GetYesNo().ToList();
                data.GalleryItemTypes = supportFactory.GetGalleryItemType().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
