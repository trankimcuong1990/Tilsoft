using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.SpecificationOfProductMng.DTO;

namespace Module.SpecificationOfProductMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchDataDTO, DTO.EditDataDTO>
    {

        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private SpecificationOfProductMngEntities CreateContext()
        {
            return new SpecificationOfProductMngEntities(Library.Helper.CreateEntityConnectionString("DAL.SpecificationOfProductMngModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ProductSpecification.Where(o => o.ProductSpecificationID == id).FirstOrDefault();
                    context.ProductSpecification.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;

            }
            catch (Exception ex)
            {

                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }

                return false;
            }

        }

        public DTO.EditDataDTO GetDataSpecification(int userId, int id, int? sampleProductID, int? productID, out Notification notification)
        {
            DTO.EditDataDTO editData = new EditDataDTO();
            editData.Data = new SpecificationOfProductDTO();
            editData.Data.specificationCushionImageDTOs = new List<SpecificationCushionImageDTO>();
            editData.Data.specificationImageDTOs = new List<SpecificationImageDTO>();
            editData.Data.specificationPackingDTOs = new List<SpecificationPackingDTO>();
            editData.Data.specificationWeavingFileDTOs = new List<SpecificationWeavingFileDTO>();
            editData.Data.specificationWoodenartDTOs = new List<SpecificationWoodenartDTO>();
            editData.Data.packingSpecificationDTOs = new List<PackingSpecificationDTO>();
            editData.Data.clientOfProductDTOs = new List<ClientOfProductDTO>();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using(var context = CreateContext())
                {
                    if (id > 0)
                    {
                        SpecificationOfProductMng_SpecificationOfProduct_View dbItem;
                        dbItem = context.SpecificationOfProductMng_SpecificationOfProduct_View
                            .Include("SpecificationOfProductMng_SpecificationCushionImage_view")
                            .Include("SpecificationOfProductMng_SpecificationImage_View")
                            .Include("SpecificationOfProductMng_SpecificationPacking_View")
                            .Include("SpecificationOfProductMng_SpecificationWeavingFile_View")
                            .Include("SpecificationOfProductMng_SpecificationWoodenPart_View").FirstOrDefault(o => o.ProductSpecificationID == id);

                        editData.Data = converter.DB2DTO_GetDataSpecificationOfProduct(dbItem);

                        var cItem = context.SpecificationOfProductMng_ClientOfProduct_View.Where(o => o.ProductID == editData.Data.ProductID);
                        editData.Data.clientOfProductDTOs = converter.DB2DTO_ClientOfProduct(cItem.ToList());
                    }
                    else
                    {
                        // Create specification wooden part
                        editData.Data.specificationWoodenartDTOs.Add(new SpecificationWoodenartDTO() { RowIndex = 1, ProductSpecificationWoodenPartID = -1, DimensionH = null, DimensionW = null, DimensionL = null, ProductSpecificationID = null, Weight = null });
                        editData.Data.specificationWoodenartDTOs.Add(new SpecificationWoodenartDTO() { RowIndex = 2, ProductSpecificationWoodenPartID = -2, DimensionH = null, DimensionW = null, DimensionL = null, ProductSpecificationID = null, Weight = null });
                        editData.Data.specificationWoodenartDTOs.Add(new SpecificationWoodenartDTO() { RowIndex = 3, ProductSpecificationWoodenPartID = -3, DimensionH = null, DimensionW = null, DimensionL = null, ProductSpecificationID = null, Weight = null });
                        editData.Data.specificationWoodenartDTOs.Add(new SpecificationWoodenartDTO() { RowIndex = 4, ProductSpecificationWoodenPartID = -4, DimensionH = null, DimensionW = null, DimensionL = null, ProductSpecificationID = null, Weight = null });
                        editData.Data.specificationWoodenartDTOs.Add(new SpecificationWoodenartDTO() { RowIndex = 5, ProductSpecificationWoodenPartID = -5, DimensionH = null, DimensionW = null, DimensionL = null, ProductSpecificationID = null, Weight = null });
                        editData.Data.specificationWoodenartDTOs.Add(new SpecificationWoodenartDTO() { RowIndex = 6, ProductSpecificationWoodenPartID = -6, DimensionH = null, DimensionW = null, DimensionL = null, ProductSpecificationID = null, Weight = null });

                        editData.Data.specificationPackingDTOs = converter.DB2DTO_GetPacking2(context.SpecificationOfProductMng_PackingSpecification_View.ToList());

                        if (sampleProductID != null)
                        {
                            var dbSample = context.SpecificationOfProductMng_SampleProduct_View.FirstOrDefault(o => o.SampleProductID == sampleProductID);

                            editData.Data.SampleProductID = dbSample.SampleProductID;
                            editData.Data.ProductUD = dbSample.SampleProductUD;
                            editData.Data.ArticleDescription = dbSample.ArticleDescription;
                            editData.Data.ClientUD = dbSample.ClientUD;
                            editData.Data.FactoryID = dbSample.VNSuggestedFactoryID;
                            editData.Data.ClientID = dbSample.ClientID;
                            editData.Data.ModelID = dbSample.ModelID;
                           
                        }

                        if(productID != null)
                        {
                            var dbProduct = context.SpecificationOfProductMng_Product_View.FirstOrDefault(o => o.ProductID == productID);

                            editData.Data.ProductID = dbProduct.ProductID;
                            editData.Data.ProductUD = dbProduct.ProductUD;
                            editData.Data.ModelID = dbProduct.ModelID;
                            editData.Data.ArticleDescription = dbProduct.ArticleDescription;
                            editData.Data.ProductOverallDimH = dbProduct.ProductOverallDimH;
                            editData.Data.ProductOverallDimL = dbProduct.ProductOverallDimL;
                            editData.Data.ProductOverallDimW = dbProduct.ProductOverallDimW;
                            editData.Data.ProductOverallWeight = dbProduct.ProductOverallWeight;
                            editData.Data.FrameMaterial = dbProduct.MaterialNM;
                            editData.Data.CushionColor = dbProduct.CushionColor;
                            editData.Data.WickerColor = dbProduct.WickerColor;
                            editData.Data.WickerType = dbProduct.WickerType;

                            var iItem = context.SpecificationOfProductMng_Product_View.Where(o=>o.ProductID == dbProduct.ProductID);
                            editData.Data.specificationImageDTOs = converter.DB2DTO_FromProductImage(iItem.ToList());

                            
                            var cItem = context.SpecificationOfProductMng_ClientOfProduct_View.Where(o => o.ProductID == dbProduct.ProductID);
                            editData.Data.clientOfProductDTOs = converter.DB2DTO_ClientOfProduct(cItem.ToList());
                        }

                    }
                }
                return editData;

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
                return editData;
            }
           
        }

        public override SearchDataDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };
            SearchDataDTO data = new SearchDataDTO();
            data.Data = new List<SpecificationOfProductSearchDTO>();

            string ProductUD = null;
            string articleDescription = null;
            string clientUD = null;
            string remark = null;

            if (filters.ContainsKey("ProductUD") && !string.IsNullOrEmpty(filters["ProductUD"].ToString()))
            {
                ProductUD = filters["ProductUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ArticleDescription") && !string.IsNullOrEmpty(filters["ArticleDescription"].ToString()))
            {
                articleDescription = filters["ArticleDescription"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                clientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Remark") && !string.IsNullOrEmpty(filters["Remark"].ToString()))
            {
                remark = filters["Remark"].ToString().Replace("'", "''");
            }

            try
            {
                using(var context = CreateContext())
                {
                    totalRows = context.SpecificationOfProductMng_Function_SearchView(ProductUD, articleDescription, clientUD, remark,  orderBy, orderDirection).Count();
                    var result = context.SpecificationOfProductMng_Function_SearchView(ProductUD, articleDescription, clientUD, remark, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }


            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.SpecificationOfProductDTO dtoItemSpec = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SpecificationOfProductDTO>();
            try
            {
                using(var context = CreateContext())
                {
                    ProductSpecification dbItem;
                    if(id == 0)
                    {
                        dbItem = new ProductSpecification();
                        context.ProductSpecification.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductSpecification.Where(o => o.ProductSpecificationID == id).FirstOrDefault();
                    }
                    if(dbItem == null)
                    {
                        notification.Message = "Data Not Found !";
                        return false;
                    }
                    else
                    {
                        //Upload File 
                        Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        //for SpecificationImage
                        foreach (DTO.SpecificationCushionImageDTO dtoSpecImage in dtoItemSpec.specificationCushionImageDTOs.Where(o => o.ScanHasChange))
                        {
                            dtoSpecImage.FileUD = fwFactory.CreateFilePointer(tempFolder, dtoSpecImage.ScanNewFile, dtoSpecImage.FileUD, dtoSpecImage.FriendlyName);
                        }
                        //for SpecificationImage
                        foreach (DTO.SpecificationImageDTO dtoSpecImage in dtoItemSpec.specificationImageDTOs.Where(o => o.ScanHasChange))
                        {
                            dtoSpecImage.FileUD = fwFactory.CreateFilePointer(tempFolder, dtoSpecImage.ScanNewFile, dtoSpecImage.FileUD, dtoSpecImage.FriendlyName);
                        }

                        //for SpecWeavingFile
                        foreach (DTO.SpecificationWeavingFileDTO dtoWeavingFile in dtoItemSpec.specificationWeavingFileDTOs.Where(o => o.ScanHasChange))
                        {
                            dtoWeavingFile.FileUD = fwFactory.CreateFilePointer(tempFolder, dtoWeavingFile.ScanNewFile, dtoWeavingFile.FileUD, dtoWeavingFile.FriendlyName);
                        }

                        converter.DTO2DB_UpdateSpecificationOfProduct(dtoItemSpec, ref dbItem);

                        //Remove
                        context.ProductSpecificationCushionImage.Local.Where(o => o.ProductSpecification == null).ToList().ForEach(o => context.ProductSpecificationCushionImage.Remove(o));
                        context.ProductSpecificationImage.Local.Where(o => o.ProductSpecification == null).ToList().ForEach(o => context.ProductSpecificationImage.Remove(o));
                        context.ProductSpecificationPacking.Local.Where(o => o.ProductSpecification == null).ToList().ForEach(o => context.ProductSpecificationPacking.Remove(o));
                        context.ProductSpecificationWeavingFile.Local.Where(o => o.ProductSpecification == null).ToList().ForEach(o => context.ProductSpecificationWeavingFile.Remove(o));
                        context.ProductSpecificationWoodenPart.Local.Where(o => o.ProductSpecification == null).ToList().ForEach(o => context.ProductSpecificationWoodenPart.Remove(o));

                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        context.SaveChanges();

                        dtoItem = GetDataSpecification(userId, dbItem.ProductSpecificationID, dbItem.ProductID, null, out notification).Data;
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

        public DTO.InitDataDTO GetInitData(int userId, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            var data = new DTO.InitDataDTO();
            data.Factories = new List<Support.DTO.Factory>();

            try
            {
                data.Factories = supportFactory.GetFactory().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data; 
        }

        // QuickSearch SampleProduct
        public List<DTO.QuickSearchSampleProductDTO> QuickSearchSample(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchSampleProductDTO> data = new List<DTO.QuickSearchSampleProductDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        public override EditDataDTO GetData(int id, out Notification notification)
        {
            DTO.EditDataDTO editData = new EditDataDTO();
            editData.Data = new SpecificationOfProductDTO();
            editData.Data.specificationCushionImageDTOs = new List<SpecificationCushionImageDTO>();
            editData.Data.specificationImageDTOs = new List<SpecificationImageDTO>();
            editData.Data.specificationPackingDTOs = new List<SpecificationPackingDTO>();
            editData.Data.specificationWeavingFileDTOs = new List<SpecificationWeavingFileDTO>();
            editData.Data.specificationWoodenartDTOs = new List<SpecificationWoodenartDTO>();
            editData.Data.packingSpecificationDTOs = new List<PackingSpecificationDTO>();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        SpecificationOfProductMng_SpecificationOfProduct_View dbItem;
                        dbItem = context.SpecificationOfProductMng_SpecificationOfProduct_View
                            .Include("SpecificationOfProductMng_SpecificationCushionImage_view")
                            .Include("SpecificationOfProductMng_SpecificationImage_View")
                            .Include("SpecificationOfProductMng_SpecificationPacking_View")
                            .Include("SpecificationOfProductMng_SpecificationWeavingFile_View")
                            .Include("SpecificationOfProductMng_SpecificationWoodenPart_View").FirstOrDefault(o => o.ProductSpecificationID == id);

                        var cItem = context.SpecificationOfProductMng_ClientOfProduct_View.Where(o => o.ProductID == dbItem.ProductID);
                        editData.Data.clientOfProductDTOs = converter.DB2DTO_ClientOfProduct(cItem.ToList());
                    }
                    editData.Data.packingSpecificationDTOs = converter.DB2DTO_GetPacking(context.SpecificationOfProductMng_PackingSpecification_View.ToList());

                    

                }
                return editData;

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
                return editData;
            }
        }

        public string GetExportData(int ProductSpecificationID, out Library.DTO.Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("SpecificationOfProductMng_Function_GetExport", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@ProductSpecificationID", ProductSpecificationID);

                adap.TableMappings.Add("Table", "SpecificationOfProductMng_SpecificationOfproduct_Rpt");
                adap.TableMappings.Add("Table1", "SpecificationOfProductMng_SpecificationImage_Rpt");
                adap.TableMappings.Add("Table2", "SpecificationOfProductMng_SpecificationWoodenPart_Rpt");
                adap.TableMappings.Add("Table3", "SpecificationOfProductMng_SpecificationWeavingFile_Rpt");
                adap.TableMappings.Add("Table4", "SpecificationOfProductMng_SpecificationCushionImage_Rpt");
                adap.TableMappings.Add("Table5", "SpecificationOfProductMng_SpecificationPacking_Rpt");

                adap.Fill(ds);
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "SpecificationOfProduct");


            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public DTO.InitDataDTO QuickSearchSample(int userId, int? factoryID, Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };
            DTO.InitDataDTO data = new DTO.InitDataDTO();
            data.Data = new List<DTO.QuickSearchSampleProductDTO>();
            try
            {
                int userID = userId;

                //if (filters.ContainsKey("factoryID") && !string.IsNullOrEmpty(filters["factoryID"].ToString()))
                //{
                //    factoryID = Convert.ToInt32(filters["factoryID"].ToString());
                //}

                string searchString = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString().Replace("'", "''"))) ? filters["searchQuery"].ToString() : null;
                
                using (var context = CreateContext())
                {
                    data.Data = converter.DB2DTO_QuickSerachSample(context.SpecificationMng_Function_QuickSearchSample(userID, factoryID, searchString).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return null;
            }
            return data;
        }

        public object GetsListSpec (int userId, int? modelID, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            DTO.SpecOfProductLists specOfProduct = new SpecOfProductLists();
            specOfProduct.Data = new List<QuickViewSpecOfProDuctListDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    specOfProduct.Data = converter.DB2DTO_GetSpecOfProductList(context.SpecificationOfProductMng_SpecificationOfProductList_View.Where(o => o.ModelID == modelID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if(ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return specOfProduct;
        }
        public object CoppySpecOfProduct (int userId, int? productSpecificationID, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            DTO.SpecOfProductCopyDTO copyData = new SpecOfProductCopyDTO();

            copyData.specificationCushionImageDTOs = new List<SpecificationCushionImageDTO>();
            copyData.specificationImageDTOs = new List<SpecificationImageDTO>();
            copyData.specificationPackingDTOs = new List<SpecificationPackingDTO>();
            copyData.specificationWeavingFileDTOs = new List<SpecificationWeavingFileDTO>();
            copyData.specificationWoodenartDTOs = new List<SpecificationWoodenartDTO>();
            copyData.packingSpecificationDTOs = new List<PackingSpecificationDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    SpecificationOfProductMng_SpecificationOfProduct_View dbItem;
                    dbItem = context.SpecificationOfProductMng_SpecificationOfProduct_View
                        .Include("SpecificationOfProductMng_SpecificationCushionImage_view")
                        .Include("SpecificationOfProductMng_SpecificationImage_View")
                        .Include("SpecificationOfProductMng_SpecificationPacking_View")
                        .Include("SpecificationOfProductMng_SpecificationWeavingFile_View")
                        .Include("SpecificationOfProductMng_SpecificationWoodenPart_View").FirstOrDefault(o => o.ProductSpecificationID == productSpecificationID);

                    copyData = converter.DB2DTO_GetcopyData(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return copyData;
        }
    }
}
