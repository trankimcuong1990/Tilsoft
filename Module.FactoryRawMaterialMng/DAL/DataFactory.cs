using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace Module.FactoryRawMaterialMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private string _tempFolder;
        private DataConverter converter = new DataConverter();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private FactoryRawMaterialMngEntities CreateContext()
        {
            return new FactoryRawMaterialMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryRawMaterialMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
             DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryRawMaterialSearch>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    string FactoryRawMaterialUD = null;
                    string FactoryRawMaterialOfficialNM = null;
                    string FactoryRawMaterialShortNM = null;
                    int? LocationID = null;
                    int? KeyRawMaterialID = null;
                    string ContactPerson = null;
                    bool? IsPotential = null;
                    string UpdatedBy = null;

                    int userId = Convert.ToInt32(filters["UserID"]);
                    int? CompanyID = fwFactory.GetCompanyID(userId);

                    if (filters.ContainsKey("FactoryRawMaterialUD") && !string.IsNullOrEmpty(filters["FactoryRawMaterialUD"].ToString()))
                    {
                        FactoryRawMaterialUD = filters["FactoryRawMaterialUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryRawMaterialOfficialNM") && !string.IsNullOrEmpty(filters["FactoryRawMaterialOfficialNM"].ToString()))
                    {
                        FactoryRawMaterialOfficialNM = filters["FactoryRawMaterialOfficialNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryRawMaterialShortNM") && !string.IsNullOrEmpty(filters["FactoryRawMaterialShortNM"].ToString()))
                    {
                        FactoryRawMaterialShortNM = filters["FactoryRawMaterialShortNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("LocationID") && !string.IsNullOrEmpty(filters["LocationID"].ToString()))
                    {
                        LocationID = Convert.ToInt32(filters["LocationID"]);
                    }
                    if (filters.ContainsKey("KeyRawMaterialID") && !string.IsNullOrEmpty(filters["KeyRawMaterialID"].ToString()))
                    {
                        KeyRawMaterialID = Convert.ToInt32(filters["KeyRawMaterialID"]);
                    }
                    if (filters.ContainsKey("ContactPerson") && !string.IsNullOrEmpty(filters["ContactPerson"].ToString()))
                    {
                        ContactPerson = filters["ContactPerson"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("IsPotential") && filters["IsPotential"] != null && !string.IsNullOrEmpty(filters["IsPotential"].ToString()))
                    {
                        IsPotential = (filters["IsPotential"].ToString() == "true") ? true : false;
                    }
                    if (filters.ContainsKey("UpdatedBy") && !string.IsNullOrEmpty(filters["UpdatedBy"].ToString()))
                    {
                        UpdatedBy = filters["UpdatedBy"].ToString().Replace("'", "''");
                    }

                    totalRows = context.FactoryRawMaterialMng_function_FactoryRawMaterial(CompanyID,FactoryRawMaterialUD, FactoryRawMaterialOfficialNM, FactoryRawMaterialShortNM, LocationID, KeyRawMaterialID, ContactPerson, IsPotential, UpdatedBy,  orderBy, orderDirection).Count();
                    var result = context.FactoryRawMaterialMng_function_FactoryRawMaterial(CompanyID,FactoryRawMaterialUD, FactoryRawMaterialOfficialNM, FactoryRawMaterialShortNM, LocationID, KeyRawMaterialID, ContactPerson, IsPotential, UpdatedBy, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryRawMaterialSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string ExportToExcel(System.Collections.Hashtable filters, int userID, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            FactoryRawMaterialMngData ds = new FactoryRawMaterialMngData();

            //try to get data
            try
            {
                string FactoryRawMaterialUD = null;
                string FactoryRawMaterialOfficialNM = null;
                string FactoryRawMaterialShortNM = null;
                int? LocationID = null;
                int? KeyRawMaterialID = null;
                string ContactPerson = null;

                int? CompanyID = fwFactory.GetCompanyID(userID);

                if (filters.ContainsKey("FactoryRawMaterialUD") && !string.IsNullOrEmpty(filters["FactoryRawMaterialUD"].ToString()))
                {
                    FactoryRawMaterialUD = filters["FactoryRawMaterialUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryRawMaterialOfficialNM") && !string.IsNullOrEmpty(filters["FactoryRawMaterialOfficialNM"].ToString()))
                {
                    FactoryRawMaterialOfficialNM = filters["FactoryRawMaterialOfficialNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryRawMaterialShortNM") && !string.IsNullOrEmpty(filters["FactoryRawMaterialShortNM"].ToString()))
                {
                    FactoryRawMaterialShortNM = filters["FactoryRawMaterialShortNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("LocationID") && !string.IsNullOrEmpty(filters["LocationID"].ToString()))
                {
                    LocationID = Convert.ToInt32(filters["LocationID"]);
                }
                if (filters.ContainsKey("KeyRawMaterialID") && !string.IsNullOrEmpty(filters["KeyRawMaterialID"].ToString()))
                {
                    KeyRawMaterialID = Convert.ToInt32(filters["KeyRawMaterialID"]);
                }
                if (filters.ContainsKey("ContactPerson") && !string.IsNullOrEmpty(filters["ContactPerson"].ToString()))
                {
                    ContactPerson = filters["ContactPerson"].ToString().Replace("'", "''");
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryRawMaterialMng_function_FactoryRawMaterialExport", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryRawMaterialUD", FactoryRawMaterialUD);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryRawMaterialOfficialNM", FactoryRawMaterialOfficialNM);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryRawMaterialShortNM", FactoryRawMaterialShortNM);
                adap.SelectCommand.Parameters.AddWithValue("@LocationID", LocationID);
                adap.SelectCommand.Parameters.AddWithValue("@KeyRawMaterialID", KeyRawMaterialID);
                adap.SelectCommand.Parameters.AddWithValue("@ContactPerson", ContactPerson);
                adap.SelectCommand.Parameters.AddWithValue("@SortedBy", orderBy);
                adap.SelectCommand.Parameters.AddWithValue("@SortedDirection", orderDirection);
                adap.TableMappings.Add("Table", "ExportToExcelFactoryRawMaterial");
               
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "FactoryRawMaterial");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return string.Empty;
            }
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    FactoryRawMaterial dbItem = context.FactoryRawMaterial.FirstOrDefault(o => o.FactoryRawMaterialID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sub Supplier not found!";
                        return false;
                    }
                    else
                    {
                        context.FactoryRawMaterial.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }
        public List<DTO.MaterialPriceProductItemSeach> GetProductionItem(string searchProductionItem, string searchProductionItemUD, int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };

            List<DTO.MaterialPriceProductItemSeach> data = new List<DTO.MaterialPriceProductItemSeach>();

            try
            {
                // Pre data param to get data ProductionItem
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    var dbItem = context.FactoryRawMaterial_function_GetProductionItemMaterial(searchProductionItem, searchProductionItemUD);
                    data = AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_SearchProductionItem_View>, List<DTO.MaterialPriceProductItemSeach>>(dbItem.ToList());
                }

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryRawMaterial dtoFactoryRawMaterial = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryRawMaterial>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //Get CompanyID
                int? companyID = fwFactory.GetCompanyID(userId);

                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    FactoryRawMaterial dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryRawMaterial();
                        context.FactoryRawMaterial.Add(dbItem);
                        var checkUD = context.FactoryRawMaterial.ToList();

                        foreach (var item in checkUD)
                        {
                            if (item.FactoryRawMaterialUD == dtoFactoryRawMaterial.FactoryRawMaterialUD)
                            {
                                throw new Exception("Sub Supplier code exists !");
                            }
                        }
                    }
                    else
                    {
                        dbItem = context.FactoryRawMaterial.FirstOrDefault(o => o.FactoryRawMaterialID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory Raw Material not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoFactoryRawMaterial.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //file processing
                        Library.FileHelper.FileManager fileMng = new Library.FileHelper.FileManager(FrameworkSetting.Setting.AbsoluteFileFolder);
                        string fileNeedDeleted = string.Empty;
                        string thumbnailFileNeedDeleted = string.Empty;

                        foreach (var contractItem in dtoFactoryRawMaterial.SubSupplierContracts.Where(o => o.ContractFileHasChange))
                        {
                            if (!string.IsNullOrEmpty(contractItem.ContractFile))
                            {
                                fwFactory.GetDBFileLocation(contractItem.ContractFile, out fileNeedDeleted, out thumbnailFileNeedDeleted);
                                if (!string.IsNullOrEmpty(fileNeedDeleted))
                                {
                                    try
                                    {
                                        fileMng.DeleteFile(fileNeedDeleted);
                                    }
                                    catch { }
                                }
                            }

                            if (string.IsNullOrEmpty(contractItem.NewContractFile))
                            {
                                // remove file registration in File table
                                fwFactory.RemoveFile(contractItem.ContractFile);

                                // reset file in table Contract
                                contractItem.ContractFile = string.Empty;
                            }
                            else
                            {
                                string outDBFileLocation = "";
                                string outFileFullPath = "";
                                string outFilePointer = "";
                                // copy new file
                                fileMng.StoreFile(this._tempFolder + contractItem.NewContractFile, out outDBFileLocation, out outFileFullPath);


                                if (File.Exists(outFileFullPath))
                                {
                                    FileInfo info = new FileInfo(outFileFullPath);

                                    // insert/update file registration in database
                                    fwFactory.UpdateFile(contractItem.ContractFile, contractItem.NewContractFile, outDBFileLocation, info.Extension, "", (int)info.Length, out outFilePointer);

                                    // set file database pointer
                                    contractItem.ContractFile = outFilePointer;
                                }
                            }

                        }

                        // processing certificate file
                        foreach (DTO.FactoryRawMaterialCertificate dtoCertificate in dtoFactoryRawMaterial.FactoryRawMaterialCertificates)
                        {
                            if (dtoCertificate.CertificateFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoCertificate.NewCertificateFile))
                                {
                                    fwFactory.RemoveImageFile(dtoCertificate.CertificateFile);
                                }
                                else
                                {
                                    dtoCertificate.CertificateFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoCertificate.NewCertificateFile, dtoCertificate.CertificateFile);
                                }
                            }
                        }
                        // Process business card image
                        foreach (DTO.FactoryRawMaterialBusinessCardDTO dtoCard in dtoFactoryRawMaterial.FactoryRawMaterialBusinessCardDTO)
                        {
                            if (dtoCard.FrontHasChange)
                            {
                                dtoCard.FrontFileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoCard.FrontNewFile, dtoCard.FrontFileUD);
                            }

                            if (dtoCard.BehindHasChange)
                            {
                                dtoCard.BehindFileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoCard.BehindNewFile, dtoCard.BehindFileUD);
                            }
                        }
                        // processing logo image
                        if (dtoFactoryRawMaterial.LogoFile_HasChange)
                        {
                            dtoFactoryRawMaterial.LogoFile = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoFactoryRawMaterial.LogoFile_NewFile, dtoFactoryRawMaterial.LogoFile);
                        }

                        // process image
                        foreach (DTO.FactoryRawMaterialImage dtoImage in dtoFactoryRawMaterial.FactoryRawMaterialImages)
                        {
                            if (dtoImage.HasChange)
                            {
                                dtoImage.FileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoImage.NewFile, dtoImage.FileUD);
                            }

                        }
                        if (dtoFactoryRawMaterial.FactoryRawMaterialGalleryDTO != null)
                        {
                            // Pre-event update FactoryGallery
                            foreach (DTO.FactoryRawMaterialGalleryDTO dtoFactoryGallery in dtoFactoryRawMaterial.FactoryRawMaterialGalleryDTO.Where(o => o.FactoryGalleryHasChange))
                            {
                                dtoFactoryGallery.FactoryRawMaterialGalleryUD = fwFactory.CreateNoneImageFilePointer(this._tempFolder, dtoFactoryGallery.FactoryGalleryNewFile, dtoFactoryGallery.FactoryRawMaterialGalleryUD);
                            }
                        }
                        // remove Technical Image
                        foreach (FactoryRawMaterialImage dbImage in context.FactoryRawMaterialImage.Local.Where(o => o.FactoryRawMaterial == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbImage.FileUD);
                            }
                        }
                        //attach file
                        foreach (var item in dtoFactoryRawMaterial.materialsPrices.Where(o => o.AttachFileHasChange))
                        {
                            
                            item.AttachFile = string.Empty;
                           
                            string outDBFileLocation = "";
                            string outFileFullPath = "";
                            string outFilePointer = "";
                            // copy new file
                            fileMng.StoreFile(this._tempFolder + item.NewAttachFile, out outDBFileLocation, out outFileFullPath);


                            if (File.Exists(outFileFullPath))
                            {
                                FileInfo info = new FileInfo(outFileFullPath);

                                // insert/update file registration in database
                                fwFactory.UpdateFile(item.AttachFile, item.NewAttachFile, outDBFileLocation, info.Extension, "", (int)info.Length, out outFilePointer);

                                // set file database pointer
                                item.AttachFile = outFilePointer;
                            }
                            

                        }
                        foreach (DTO.MaterialsPrice item in dtoFactoryRawMaterial.materialsPrices)
                        {
                           
                                if (item.IsChange == true)
                                {
                                    MaterialPriceHistory dbItemPriceHistory = new MaterialPriceHistory();
                                    dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                    dbItemPriceHistory.Price = (decimal)item.OldPrice;
                                    dbItemPriceHistory.UpdatedBy = userId;
                                    dbItemPriceHistory.UpdatedDate = DateTime.Now;


                                    dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                    dbItemPriceHistory.Qty = (decimal)item.OldQty;
                                    dbItemPriceHistory.UpdatedBy = userId;
                                    dbItemPriceHistory.UpdatedDate = DateTime.Now;

                                    dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                    dbItemPriceHistory.StatusID = (int)item.OldStatusID;
                                    dbItemPriceHistory.UpdatedBy = userId;
                                    dbItemPriceHistory.UpdatedDate = DateTime.Now;

                                    dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                    dbItemPriceHistory.ValidFrom = Convert.ToDateTime(item.OldValidFrom);
                                    dbItemPriceHistory.UpdatedBy = userId;
                                    dbItemPriceHistory.UpdatedDate = DateTime.Now;

                                    dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                    dbItemPriceHistory.AttachFileHistory = item.OldAttachFile;
                                    dbItemPriceHistory.UpdatedBy = userId;
                                    dbItemPriceHistory.UpdatedDate = DateTime.Now;

                                    dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                    dbItemPriceHistory.RemarkHistory = item.OldRemark;
                                    dbItemPriceHistory.UpdatedBy = userId;
                                    dbItemPriceHistory.UpdatedDate = DateTime.Now;
                                    context.MaterialPriceHistory.Add(dbItemPriceHistory);
                                }
                                else if (item.AttachFile != item.OldAttachFile)
                                 {
                                MaterialPriceHistory dbItemPriceHistory = new MaterialPriceHistory();
                                dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                dbItemPriceHistory.Price = (decimal)item.OldPrice;
                                dbItemPriceHistory.UpdatedBy = userId;
                                dbItemPriceHistory.UpdatedDate = DateTime.Now;


                                dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                dbItemPriceHistory.Qty = (decimal)item.OldQty;
                                dbItemPriceHistory.UpdatedBy = userId;
                                dbItemPriceHistory.UpdatedDate = DateTime.Now;

                                dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                dbItemPriceHistory.StatusID = (int)item.OldStatusID;
                                dbItemPriceHistory.UpdatedBy = userId;
                                dbItemPriceHistory.UpdatedDate = DateTime.Now;

                                dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                dbItemPriceHistory.ValidFrom = Convert.ToDateTime(item.OldValidFrom);
                                dbItemPriceHistory.UpdatedBy = userId;
                                dbItemPriceHistory.UpdatedDate = DateTime.Now;

                                dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                dbItemPriceHistory.AttachFileHistory = item.OldAttachFile;
                                dbItemPriceHistory.UpdatedBy = userId;
                                dbItemPriceHistory.UpdatedDate = DateTime.Now;

                                dbItemPriceHistory.MaterialsPriceID = (int)item.MaterialsPriceID;
                                dbItemPriceHistory.RemarkHistory = item.OldRemark;
                                dbItemPriceHistory.UpdatedBy = userId;
                                dbItemPriceHistory.UpdatedDate = DateTime.Now;
                                context.MaterialPriceHistory.Add(dbItemPriceHistory);
                                 }
                        }
                        
                        //convert dto to db
                        converter.DTO2BD(dtoFactoryRawMaterial, ref dbItem);
                        if (!string.IsNullOrEmpty(dbItem.WebAddress))
                        {
                            dbItem.WebAddress = dbItem.WebAddress.ToLower().Replace("http://", "").Replace("https://", "");
                        }

                        // FactoryRawMaterialCertificate
                        context.FactoryRawMaterialCertificate.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.FactoryRawMaterialCertificate.Remove(o));
                        // FactoryInHouseTest
                        context.FactoryRawMaterialTest.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.FactoryRawMaterialTest.Remove(o));
                        // FactoryRawMaterialPricingPerson
                        context.FactoryRawMaterialPricingPerson.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.FactoryRawMaterialPricingPerson.Remove(o));
                        // FactoryRawMaterialPricingPerson
                        context.FactoryRawMaterialQualityPerson.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.FactoryRawMaterialQualityPerson.Remove(o));
                        // FactoryRawMaterialPaymentTerm
                        context.FactoryRawMaterialPaymentTerm.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.FactoryRawMaterialPaymentTerm.Remove(o));

                        // FactoryRawMaterialMng_SupplierContactQuickInfo_View
                        context.SupplierContactQuickInfo.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.SupplierContactQuickInfo.Remove(o));

                        context.SupplierDirector.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.SupplierDirector.Remove(o));

                        context.SupplierManager.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.SupplierManager.Remove(o));

                        context.SupplierSampleTechnical.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.SupplierSampleTechnical.Remove(o));

                        context.MaterialsPrice.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.MaterialsPrice.Remove(o));

                        context.MaterialPriceHistory.Local.Where(o => o.MaterialsPrice == null).ToList().ForEach(o => context.MaterialPriceHistory.Remove(o));

                        context.FactoryRawMaterialBusinessCard.Local.Where(o => o.FactoryRawMaterial == null).ToList().ForEach(o => context.FactoryRawMaterialBusinessCard.Remove(o));

                        // FactoryGallery for Factory(remove value Factory is null)
                        foreach (FactoryRawMaterialGallery dbFactoryGallery in context.FactoryRawMaterialGallery.Where(o => o.FactoryRawMaterial == null).ToList())
                        {
                            fwFactory.RemoveImageFile(dbFactoryGallery.FactoryRawMaterialGalleryUD);
                            context.FactoryRawMaterialGallery.Remove(dbFactoryGallery);
                        }

                        dbItem.CompanyID = companyID;
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;
                        
                        context.SaveChanges();


                        dtoItem = GetData(userId, dbItem.FactoryRawMaterialID, out notification).Data;

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

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
    
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryRawMaterial();
            data.Data.FactoryRawMaterialCertificates = new List<DTO.FactoryRawMaterialCertificate>();
            data.Data.FactoryRawMaterialTests = new List<DTO.FactoryRawMaterialTest>();
            data.Data.FactoryRawMaterialTurnovers = new List<DTO.FactoryRawMaterialTurnover>();
            data.Data.FactoryRawMaterialPricingPersons = new List<DTO.FactoryRawMaterialPricingPerson>();
            data.Data.FactoryRawMaterialQualityPersons = new List<DTO.FactoryRawMaterialQualityPerson>();
            data.Data.FactoryRawMaterialImages = new List<DTO.FactoryRawMaterialImage>();
            data.Data.FactoryRawPaymentTerms = new List<DTO.FactoryRawPaymentTerm>();
            data.Data.SubSupplierContracts = new List<DTO.SubSupplierContract>();
            data.Data.SupplierContactQuickInfos = new List<DTO.SupplierContactQuickInfo>();
            data.Data.supplierDirectors = new List<DTO.SupplierDirector>();
            data.Data.supplierManagers = new List<DTO.SupplierManager>();
            data.Data.supplierSampleTechnicals = new List<DTO.SupplierSampleTechnical>();
            data.ProductGroupDTOs = new List<DTO.ProductGroupDTO>();
            data.Data.FactoryRawMaterialProductGroupDTOs = new List<DTO.FactoryRawMaterialProductGroupDTO>();

            data.materialsPrices = new List<DTO.MaterialsPrice>();
            data.Data.materialsPrices = new List<DTO.MaterialsPrice>();



            data.statusMaterialPrices = new List<DTO.StatusMaterialPrice>();
            data.Data.statusMaterials = new List<DTO.StatusMaterialPrice>();

            data.KeyRawMaterials = new List<DTO.KeyRawMaterial>();
            data.Locations = new List<Support.DTO.FactoryLocation>();
            data.Employees = new List<Support.DTO.Employee>();
            data.SupplierPaymentTerms = new List<DTO.SupplierPaymentTerm>();
            data.SubSupplierDocumentTypeDTOs = new List<DTO.SubSupplierDocumentTypeDTO>();
            data.AppointmentDTO = new List<DTO.AppointmentDTO>();
            data.Data.FactoryRawMaterialBusinessCardDTO = new List<DTO.FactoryRawMaterialBusinessCardDTO>();
            data.UsersDTO = new List<DTO.UsersDTO>();
            data.Data.FactoryRawMaterialGalleryDTO = new List<DTO.FactoryRawMaterialGalleryDTO>();
            //try to get data
            try
            {              
                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        int? companyID = fwFactory.GetCompanyID(userId);
                        List<int> internalCompanyIDs = new List<int> { 1, 2, 3, 4, 11, 13, 75 };
                        var factoryRaw = context.FactoryRawMaterialMng_FactoryRawMaterial_View.FirstOrDefault(o => o.FactoryRawMaterialID == id);
                        if (internalCompanyIDs.Contains(companyID.Value))
                        {
                            if (!internalCompanyIDs.Contains(factoryRaw.CompanyID.Value))
                            {
                                factoryRaw = null;
                            }
                        }
                        else
                        {
                            if (factoryRaw.CompanyID.Value != companyID.Value)
                            {
                                factoryRaw = null;
                            }
                        }
                        
                        if (factoryRaw == null)
                        {
                            throw new Exception("Can not found the data to edit");
                        }

                        data.Data = converter.DB2DTO_FactoryRawMaterial(factoryRaw);                        
                    }
                    data.SupplierPaymentTerms = converter.DB2DTO_SupplierPaymentTermList(context.FactoryRawMaterialMng_SupportSupplierPaymentTerm_View.ToList());
                    data.SubSupplierDocumentTypeDTOs = converter.DB2DTO_SubSupplierDocumentTypeDTO(context.SupportMng_SubSupplierDocumentType_View.ToList());
                    data.AppointmentDTO = converter.DB2DTO_SubSupplierAppointmentDTO(context.PurchasingCalendarMng_PurchasingCalendarAppointment_View.ToList());                    
                    data.UsersDTO = converter.DB2DTO_UsersDTO(context.SupportMng_User2_View.ToList());
                }
                data.Locations = supportFactory.GetFactoryLocation();
                data.KeyRawMaterials = GetKeyMaterialList();
                data.Employees = supportFactory.GetEmployee();
                data.ProductGroupDTOs = GetProductGroupList();
                data.statusMaterialPrices = GetStatusMaterialPrices();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Locations = new List<Support.DTO.FactoryLocation>();
            data.KeyRawMaterials = new List<DTO.KeyRawMaterial>();
            try
            {
                data.Locations = supportFactory.GetFactoryLocation();
                data.KeyRawMaterials = GetKeyMaterialList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public List<DTO.StatusMaterialPrice> GetStatusMaterialPrices()
        {
            List<DTO.StatusMaterialPrice> result = new List<DTO.StatusMaterialPrice>();
            try
            {
                using (FactoryRawMaterialMngEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_StatusMaterailPrice(context.FactoryRawMaterialMng_StatusMaterialsPrice_View.ToList());
                }
            }
            catch { }

            return result;
        }
        public List<DTO.MaterialsPrice> GetMaterialPrices()
        {
            List<DTO.MaterialsPrice> result = new List<DTO.MaterialsPrice>();
            try
            {
                using (FactoryRawMaterialMngEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_MaterailPrice(context.FactoryRawMaterialMng_MaterialsPrice_View.ToList());
                }
            }
            catch { }

            return result;
        }
        public DTO.EditFormData GetDetail(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            if (id > 0)
            {
                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    var factoryRaw = context.FactoryRawMaterialMng_FactoryRawMaterial_View.FirstOrDefault(o => o.FactoryRawMaterialID == id);
                    if (factoryRaw == null)
                    {
                        throw new Exception("Can not found the data to edit");
                    }

                    data.Data = converter.DB2DTO_FactoryRawMaterial(factoryRaw);
                }
            }


            return data;
        }

        public List<DTO.ProductGroupDTO> GetProductGroupList()
        {
            List<DTO.ProductGroupDTO> result = new List<DTO.ProductGroupDTO>();
            try
            {
                using (FactoryRawMaterialMngEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_ProductGroup(context.SupportMng_ProductGroup_View.ToList());
                }
            }
            catch { }

            return result;
        }

        // Add new key raw material
        public List<DTO.KeyRawMaterial> UpdateRawList(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.KeyRawMaterial> dtoKeyRawMaterialList = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.KeyRawMaterial>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.KeyRawMaterial> DTOKeyRawMaterials = new List<DTO.KeyRawMaterial>();

            try
            {
                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    var dbItemList = context.KeyRawMaterial.Select(o => o).ToList();
                    //converter.DTO2DB_KeyRawMaterial(dtoKeyRawMaterialList, ref dbItemList);

                    //Delete orphan
                    foreach (var item in dbItemList.ToArray())
                    {
                        if (!dtoKeyRawMaterialList.Select(o => o.KeyRawMaterialID).Contains(item.KeyRawMaterialID))
                        {
                            context.KeyRawMaterial.Remove(item);
                        }
                    }

                    //Map child row
                    foreach (var item in dtoKeyRawMaterialList)
                    {
                        KeyRawMaterial dbKeyRawMaterial;
                        if (item.KeyRawMaterialID <= 0)
                        {
                            dbKeyRawMaterial = new KeyRawMaterial();
                            context.KeyRawMaterial.Add(dbKeyRawMaterial);
                        }
                        else
                        {
                            dbKeyRawMaterial = context.KeyRawMaterial.FirstOrDefault(o => o.KeyRawMaterialID == item.KeyRawMaterialID);
                        }

                        if (dbKeyRawMaterial != null)
                        {
                            AutoMapper.Mapper.Map<DTO.KeyRawMaterial, KeyRawMaterial>(item, dbKeyRawMaterial);
                        }
                    }

                    context.SaveChanges();
                }
                DTOKeyRawMaterials = GetKeyMaterialList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return DTOKeyRawMaterials;
        }

        // Add new key raw material
        public int? RemoveRaw(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.KeyRawMaterial dtoRemovedRaw = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.KeyRawMaterial>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int? IsOkToDelele = null;
            try
            {
                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    var result = context.FactoryRawMaterial.FirstOrDefault(o => o.KeyRawMaterialID == dtoRemovedRaw.KeyRawMaterialID);
                    if (result == null)
                    {
                        IsOkToDelele = 1;
                    }
                    else
                    {
                        IsOkToDelele = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return IsOkToDelele;
        }

        public List<DTO.KeyRawMaterial> GetKeyMaterialList()
        {
            List<DTO.KeyRawMaterial> result = new List<DTO.KeyRawMaterial>();
            try
            {
                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    result = converter.DB2DTO_KeyRawMaterial(context.FactoryRawMaterialMng_KeyRawMaterial_View.ToList());
                }
            }
            catch { }

            return result;
        }

        // Get overview
        public DTO.EditFormData GetOverview(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            if (id > 0)
            {
                using (FactoryRawMaterialMngEntities context = CreateContext())
                {
                    var factoryRaw = context.FactoryRawMaterialMng_FactoryRawMaterial_View.FirstOrDefault(o => o.FactoryRawMaterialID == id);
                    if (factoryRaw == null)
                    {
                        throw new Exception("Can not found the data to edit");
                    }

                    data.Data = converter.DB2DTO_FactoryRawMaterial(factoryRaw);
                }
            }
            else
            {
                throw new Exception("Can not found the data to view");
            }
            return data;
        }
    }
}
