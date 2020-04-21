using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.ProductMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ProductMng.SearchFormData, DTO.ProductMng.EditFormData, DTO.ProductMng.Product>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private string _TempFolder;
        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private ProductMngEntities CreateContext()
        {
            return new ProductMngEntities(DALBase.Helper.CreateEntityConnectionString("ProductMngModel"));
        }

        public override DTO.ProductMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProductMng.SearchFormData data = new DTO.ProductMng.SearchFormData();
            data.Data = new List<DTO.ProductMng.ProductSearchResult>();
            totalRows = 0;
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    string articleCode = null;
                    string description = null;
                    string season = null;
                    int? cataloguePageNumber = null;
                    bool? isConfirmed = null;
                    int? productTypeID = null;
                    string eanCode = null;

                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        articleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        description = filters["Description"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        season = filters["Season"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("CataloguePageNumber") && !string.IsNullOrEmpty(filters["CataloguePageNumber"].ToString()))
                    {
                        cataloguePageNumber = Convert.ToInt32(filters["CataloguePageNumber"]);
                    }

                    if (filters.ContainsKey("IsConfirmed") && !string.IsNullOrEmpty(filters["IsConfirmed"].ToString()))
                    {
                        isConfirmed = Convert.ToBoolean(filters["IsConfirmed"].ToString());
                    }

                    if (filters.ContainsKey("ProductTypeID") && !string.IsNullOrEmpty(filters["ProductTypeID"].ToString()))
                    {
                        productTypeID = Convert.ToInt32(filters["ProductTypeID"].ToString());
                    }

                    if (filters.ContainsKey("EANCode") && !string.IsNullOrEmpty(filters["EANCode"].ToString()))
                    {
                        eanCode = filters["EANCode"].ToString().Replace("'", "''");
                    }
                    totalRows = context.ProductMng_function_SearchProduct(articleCode, description, season, cataloguePageNumber, isConfirmed, productTypeID, eanCode, orderBy, orderDirection).Count();
                    var result = context.ProductMng_function_SearchProduct(articleCode, description, season, cataloguePageNumber, isConfirmed, productTypeID, eanCode, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ProductSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());                    
                    //get EANCode for products
                    List<int> productIDs = new List<int>();
                    productIDs = data.Data.Select(o => o.ProductID).ToList();
                    var EANCodes = context.ProductMng_ProductSetEANCode_View.Where(o => productIDs.Contains(o.ProductID.Value));
                    foreach (var pItem in data.Data)
                    {
                        pItem.EANCode = "";
                        foreach (var eItem in EANCodes.Where(o => o.ProductID == pItem.ProductID))
                        {
                            pItem.EANCode += eItem.EANCode + ",";
                        }
                        if (pItem.EANCode.Length > 1)
                        {
                            pItem.EANCode = pItem.EANCode.Substring(0, pItem.EANCode.Length - 1);
                        }
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
            }
            return data;
        }

        public override DTO.ProductMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    Product dbItem = context.Product.FirstOrDefault(o => o.ProductID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Product not found!";
                        return false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dbItem.ImageFile))
                        {
                            // remove image
                            fwFactory.RemoveImageFile(dbItem.ImageFile);
                        }

                        context.Product.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.ProductMng.Product dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    Product dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Product();
                        dbItem.CreatedBy = dtoItem.CreatedBy;
                        dbItem.CreatedDate = DateTime.Now;
                        context.Product.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Product.FirstOrDefault(o => o.ProductID == id);
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Product not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB(dtoItem, ref dbItem);

                        // processing image
                        if (dtoItem.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
                            dbItem.IsMatchedImage = true;
                        }

                        context.ProductPiece.Local.Where(o => o.Product == null).ToList().ForEach(o => context.ProductPiece.Remove(o));
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ProductID, 0, out notification).Data;
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

        public override bool Approve(int id, ref DTO.ProductMng.Product dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    Product product = context.Product.FirstOrDefault(o => o.ProductID == id);

                    if (!product.MaterialID.HasValue || !product.ModelID.HasValue || !product.MaterialTypeID.HasValue || !product.MaterialColorID.HasValue || !product.ManufacturerCountryID.HasValue || !product.FrameMaterialID.HasValue || !product.FrameMaterialColorID.HasValue || !product.SubMaterialColorID.HasValue || !product.SubMaterialID.HasValue || !product.CushionColorID.HasValue || !product.CushionID.HasValue)
                    {
                        throw new Exception("Please fill in all the required data for current product - Model, Material, Cushion, Frame and Manufacturer Country is required");
                    }

                    product.IsConfirmed = true;
                    product.ConfirmedDate = DateTime.Now;
                    product.ConfirmedBy = dtoItem.UpdatedBy;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Reset(int id, ref DTO.ProductMng.Product dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    Product product = context.Product.FirstOrDefault(o => o.ProductID == id);
                    product.IsConfirmed = false;
                    product.ConfirmedDate = null;
                    product.ConfirmedBy = null;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.ProductMng.EditFormData GetData(int id, int modelID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProductMng.EditFormData data = new DTO.ProductMng.EditFormData();
            data.Data = new DTO.ProductMng.Product() { ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg" };
            data.Seasons = new List<DTO.Support.Season>();
            data.ModelPackagingMethodOption2DTOs = new List<DTO.ProductMng.ModelPackagingMethodOption2DTO>();
            data.PackagingMethodOptions = new List<DTO.Support.ModelPackagingMethodOption>();
            data.Packages = new List<DTO.ProductMng.Package>();

            //try to get data
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    //data.PackagingMethodOptions = supportFactory.GetModelPackagingMethodOption(modelID).ToList();
                    data.Seasons = supportFactory.GetSeason().ToList();
                    data.Packages = converter.DB2DTO_Package(context.ProductMng_Package_Constant_View.ToList());

                    //ecommerce spec
                    data.Materials = supportFactory.GetMaterial().ToList();
                    data.FrameMaterials = supportFactory.GetFrameMaterial().ToList();
                    data.MaterialTypes = supportFactory.GetMaterialType().ToList();
                    data.FSCTypes = supportFactory.GetFSCType().ToList();
                    data.MaterialColors = supportFactory.GetMaterialColor().ToList();
                    data.FrameMaterialColors = supportFactory.GetFrameMaterialColor().ToList();
                    data.CushionColors = supportFactory.GetCushionColor().ToList();
                    data.CushionTypes = supportFactory.GetCushionType().ToList();

                    // add new case
                    if (id == 0)
                    {
                        ProductMng_Model_View dbModel = context.ProductMng_Model_View.FirstOrDefault(o => o.ModelID == modelID);
                        if (dbModel != null)
                        {
                            DTO.ProductMng.Product oProduct = data.Data;
                            converter.DB2DTO_Model2Product(dbModel, ref oProduct);
                            data.Data.ArticleCode = dbModel.ModelUD + "********************";
                            data.Data.Description = dbModel.ModelNM;
                            data.Data.Season = Library.OMSHelper.Helper.GetCurrentSeason();
                            data.Data.ProductTypeNM = dbModel.ProductTypeNM;
                            data.Data.HSCode = dbModel.HSCode;

                            //init product ecommerce spec
                            oProduct.ProductECommerceSpecs = new List<DTO.ProductMng.ProductECommerceSpec>();
                            DTO.ProductMng.ProductECommerceSpec spec = new DTO.ProductMng.ProductECommerceSpec();
                            spec.ProductECommerceSpecID = -1;
                            oProduct.ProductECommerceSpecs.Add(spec);
                        }
                        else
                        {
                            throw new Exception("Model not exists!");
                        }

                        data.PackagingMethodOptions = supportFactory.GetModelPackagingMethodOption(modelID).ToList();
                        var packingMethods = context.ProductMng_function_getPackagingMethod(modelID).ToList();
                        data.ModelPackagingMethodOption2DTOs = converter.DB2DTO_ModelPackagingMethodOption2(packingMethods);
                        
                    }
                    else
                    {
                        int ProductOffer = 0;
                        //create pieces for  product
                        context.ProductMng_function_CreateProductPiece(id);

                        //create product ecommerce spec
                        context.ProductMng_function_CreateProductECommerceSpec(id);

                        //get data
                        data.Data = converter.DB2DTO_Product(context.ProductMng_Product_View.Include("ProductMng_ProductPiece_View")
                                                                                            .Include("ProductMng_ProductSetEANCode_View.ProductMng_ProductColli_View.ProductMng_ProductColliPiece_View")
                                                                                            .Include("ProductMng_ProductECommerceSpec_View")
                                                                                            .FirstOrDefault(o => o.ProductID == id));

                        ProductOffer = context.ProductMng_CheckProductInOffer_View.Where(o => o.ArticleCode == data.Data.ArticleCode).Count();
                        if(ProductOffer > 0)
                        {
                            data.Data.IsOffer = true;
                        }
                        else
                        {
                            data.Data.IsOffer = false;
                        }

                        if (data.Data.ConcurrencyFlag != null)
                        {
                            data.Data.ConcurrencyFlag_String = Convert.ToBase64String(data.Data.ConcurrencyFlag);
                        }

                        data.PackagingMethodOptions = supportFactory.GetModelPackagingMethodOption(data.Data.ModelID).ToList();
                        var packingMethods = context.ProductMng_function_getPackagingMethod(data.Data.ModelID).ToList();
                        data.ModelPackagingMethodOption2DTOs = converter.DB2DTO_ModelPackagingMethodOption2(packingMethods);
                    }

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.ProductMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProductMng.SearchFilterData data = new DTO.ProductMng.SearchFilterData();
            data.ConfirmStatuses = new List<DTO.Support.YesNo>();
            data.ProductTypes = new List<DTO.Support.ProductType>();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.ProductTypes = supportFactory.GetProductType().ToList();
                data.ConfirmStatuses = supportFactory.GetYesNo().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        //generate EAN Code

        public bool GeneratePieceCode(int id, out Library.DTO.Notification notification)
        {
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (ProductMngEntities context = CreateContext())
            //    {
            //        //auto create ean code for product which is set
            //        var dbProduct = context.Product.Where(o => o.ProductID == id).FirstOrDefault();
            //        var dbModel = context.ProductMng_Model_View.Where(o => o.ModelID == dbProduct.ModelID).FirstOrDefault();
            //        if (dbModel.ProductTypeID == 5) // generate EAN Code for set
            //        {
            //            if (string.IsNullOrEmpty(dbProduct.EANCode))
            //            {
            //                var x = context.Product.OrderByDescending(o => o.EANCodeIndex).First();
            //                int i = (x.EANCodeIndex.HasValue ? x.EANCodeIndex.Value : 0) + 1;
            //                dbProduct.EANCodeIndex = i;
            //                dbProduct.EANCode = "871969928" + i.ToString().PadLeft(4, '0');
            //            }
            //        }
            //        context.SaveChanges();
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return false;
            //}
            throw new NotImplementedException();
        }

        public bool GeneratePreparingDataPieceEANCode(int id, out Library.DTO.Notification notification)
        {
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    //update data first
            //    using (ProductMngEntities context = CreateContext())
            //    {
            //        var dbProduct = context.Product.Where(o => o.ProductID == id).FirstOrDefault();
            //        //auto create ean code for product which is set
            //        var dbModel = context.ProductMng_Model_View.Where(o => o.ModelID == dbProduct.ModelID).FirstOrDefault();
            //        if (dbModel.ProductTypeID == 5) // generate EAN Code for set
            //        {
            //            if (string.IsNullOrEmpty(dbProduct.EANCode))
            //            {
            //                var x = context.Product.OrderByDescending(o => o.EANCodeIndex).First();
            //                int i = (x.EANCodeIndex.HasValue ? x.EANCodeIndex.Value : 0) + 1;
            //                dbProduct.EANCodeIndex = i;
            //                dbProduct.EANCode = "871969928" + i.ToString().PadLeft(4, '0');
            //            }
            //        }                    
            //        //remove orphan item                   
            //        context.SaveChanges();
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return false;
            //}      
            throw new NotImplementedException();
        }

        public bool GenerateEANCode(int id, out Library.DTO.Notification notification)
        {
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (ProductMngEntities context = CreateContext())
            //    {
            //        var dbProduct = context.Product.Where(o => o.ProductID == id).FirstOrDefault();
            //        //auto create ean code for product which is set
            //        var dbModel = context.ProductMng_Model_View.Where(o => o.ModelID == dbProduct.ModelID).FirstOrDefault();
            //        if (dbModel.ProductTypeID == 5) // generate EAN Code for set
            //        {
            //            if (string.IsNullOrEmpty(dbProduct.EANCode))
            //            {
            //                var x = context.Product.OrderByDescending(o => o.EANCodeIndex).First();
            //                int i = (x.EANCodeIndex.HasValue ? x.EANCodeIndex.Value : 0) + 1;
            //                dbProduct.EANCodeIndex = i;
            //                dbProduct.EANCode = "871969928" + i.ToString().PadLeft(4, '0');
            //            }
            //        }
            //        context.SaveChanges();
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return false;
            //}   
            throw new NotImplementedException();
        }

        public bool GenerateEANCodeForSET(int id, out Library.DTO.Notification notification)
        {
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (ProductMngEntities context = CreateContext())
            //    {
            //        //auto create ean code for product which is set
            //        var dbProduct = context.Product.Where(o => o.ProductID == id).FirstOrDefault();
            //        var dbModel = context.ProductMng_Model_View.Where(o => o.ModelID == dbProduct.ModelID).FirstOrDefault();
            //        if (dbModel.ProductTypeID == 5) // generate EAN Code for set
            //        {
            //            if (string.IsNullOrEmpty(dbProduct.EANCode))
            //            {
            //                var x = context.Product.OrderByDescending(o => o.EANCodeIndex).First();
            //                int i = (x.EANCodeIndex.HasValue ? x.EANCodeIndex.Value : 0) + 1;
            //                dbProduct.EANCodeIndex = i;
            //                dbProduct.EANCode = "871969928" + i.ToString().PadLeft(4, '0');
            //            }
            //        }
            //        context.SaveChanges();
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return false;
            //}
            throw new NotImplementedException();
        }

        public bool ResetEANCodeForSET(int id, out Library.DTO.Notification notification)
        {
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (ProductMngEntities context = CreateContext())
            //    {
            //        var dbProduct = context.Product.Where(o => o.ProductID == id).FirstOrDefault();
            //        var dbModel = context.ProductMng_Model_View.Where(o => o.ModelID == dbProduct.ModelID).FirstOrDefault();
            //        if (dbModel.ProductTypeID == 5) // generate EAN Code for set
            //        {
            //            if (!string.IsNullOrEmpty(dbProduct.EANCode))
            //            {
            //                dbProduct.EANCode = "";
            //                dbProduct.EANCodeIndex = null;
            //                context.SaveChanges();
            //            }
            //        }                    
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return false;
            //}
            throw new NotImplementedException();
        }

        //ean code new feature

        public DTO.ProductMng.ProductSetEANCode CreateSetEanCode(int productID, string eanCode, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Create set ean code success" };
            DTO.ProductMng.ProductSetEANCode dtoSetEanCode = new DTO.ProductMng.ProductSetEANCode();
            dtoSetEanCode.ProductSetEANCodeID = -1;
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    var p = context.Product.Where(o => o.ProductID == productID).FirstOrDefault();
                    ProductSetEANCode dbSetEanCode = new ProductSetEANCode();
                    p.ProductSetEANCode.Add(dbSetEanCode);

                    //if (string.IsNullOrEmpty(eanCodeIndex))
                    //{
                    //    //finding lasted eancode
                    //    var x = context.ProductSetEANCode.OrderByDescending(o => o.EANCodeIndex).FirstOrDefault();
                    //    int i = (x == null ? 1 : (x.EANCodeIndex.HasValue ? x.EANCodeIndex.Value : 0) + 1);
                    //    dbSetEanCode.EANCodeIndex = i;
                    //    dbSetEanCode.EANCode = "871969928" + i.ToString().PadLeft(4, '0');
                    //}
                    //else
                    //{
                    //    dbSetEanCode.EANCode = "871969928" + eanCodeIndex;
                    //    dbSetEanCode.EANCodeIndex = Convert.ToInt32(eanCodeIndex);
                    //}

                    dbSetEanCode.EANCode = eanCode;
                    dbSetEanCode.EANCodeIndex = Convert.ToInt32(eanCode.Substring(9, 4));
                    context.SaveChanges();
                    //create dto to return
                    dtoSetEanCode = new DTO.ProductMng.ProductSetEANCode();
                    dtoSetEanCode.ProductSetEANCodeID = dbSetEanCode.ProductSetEANCodeID;
                    dtoSetEanCode.EANCode = dbSetEanCode.EANCode;
                    dtoSetEanCode.EANCodeIndex = dbSetEanCode.EANCodeIndex;
                }
                return dtoSetEanCode;
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
                return dtoSetEanCode;
            }
        }

        public DTO.ProductMng.ProductColli CreateColli(int productSetEANCodeID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Create set ean code success" };
            DTO.ProductMng.ProductColli dtoColli = new DTO.ProductMng.ProductColli();
            dtoColli.ProductColliID = -1;
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    var dbSetEanCode = context.ProductSetEANCode.Where(o => o.ProductSetEANCodeID == productSetEANCodeID).FirstOrDefault();
                    ProductColli dbColli = new ProductColli();
                    dbSetEanCode.ProductColli.Add(dbColli);

                    //finding lasted eancode
                    var x = dbSetEanCode.ProductColli.OrderByDescending(o => o.ColliIndex).First();
                    int i = (x.ColliIndex.HasValue ? x.ColliIndex.Value : 0) + 1;
                    dbColli.ColliIndex = i;
                    dbColli.EANCode = dbSetEanCode.EANCode + "-" + i.ToString().PadLeft(2, '0');
                    context.SaveChanges();
                    dtoColli.ProductColliID = dbColli.ProductColliID;
                    dtoColli.EANCode = dbColli.EANCode;
                    dtoColli.ColliIndex = dbColli.ColliIndex;

                    if (i == 1)
                    {
                        var dbProduct = context.ProductMng_Product_View.FirstOrDefault(o => o.ProductID == dbSetEanCode.ProductID);

                        if (dbProduct != null)
                        {
                            dtoColli.PackagingMethodID = dbProduct.PackagingMethodID;
                            dtoColli.CartonBoxDimH = dbProduct.CartonBoxDimH;
                            dtoColli.CartonBoxDimL = dbProduct.CartonBoxDimL;
                            dtoColli.CartonBoxDimW = dbProduct.CartonBoxDimW;
                            dtoColli.NetWeight = dbProduct.NetWeight2;
                            dtoColli.GrossWeight = dbProduct.GrossWeight2;
                        }
                    }
                }

                return dtoColli;
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
                return dtoColli;
            }
        }

        public object UpdateColli(int productColliID, DTO.ProductMng.ProductColli dataItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.ProductMng.ProductColli dtoItem = new DTO.ProductMng.ProductColli();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ProductColli.FirstOrDefault(o => o.ProductColliID == productColliID);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can't find data ProductColli";
                        return null;
                    }

                    dbItem.PackagingMethodID = dataItem.PackagingMethodID;
                    dbItem.CartonBoxDimH = dataItem.CartonBoxDimH;
                    dbItem.CartonBoxDimL = dataItem.CartonBoxDimL;
                    dbItem.CartonBoxDimW = dataItem.CartonBoxDimW;
                    dbItem.NetWeight = dataItem.NetWeight;
                    dbItem.GrossWeight = dataItem.GrossWeight;

                    context.SaveChanges();

                    dtoItem = AutoMapper.Mapper.Map<ProductMng_ProductColli_View, DTO.ProductMng.ProductColli>(context.ProductMng_ProductColli_View.FirstOrDefault(o => o.ProductColliID == dbItem.ProductColliID));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }

        public DTO.ProductMng.ProductColliPiece CreateColliPiece(int productColliID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Create set ean code success" };
            DTO.ProductMng.ProductColliPiece dtoColliPiece = new DTO.ProductMng.ProductColliPiece();
            dtoColliPiece.ProductColliPieceID = -1;
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    var dbColli = context.ProductColli.Where(o => o.ProductColliID == productColliID).FirstOrDefault();
                    ProductColliPiece dbColliPiece = new ProductColliPiece();
                    dbColli.ProductColliPiece.Add(dbColliPiece);
                    dbColliPiece.Pcs = 1;
                    dbColliPiece.Colli = 1;

                    //if product is not SET then PieceID is same ProductID
                    int? productID = dbColli.ProductSetEANCode.ProductID;
                    var x = context.ProductMng_Product_View.Where(o => o.ProductID == productID).FirstOrDefault();
                    if (x == null)
                    {
                        throw new Exception("Could not find product type of product");
                    }
                    string productTypeNM = x.ProductTypeNM;
                    if (productTypeNM != "SET")
                    {
                        dbColliPiece.PieceID = productID;
                    }
                    //save data
                    context.SaveChanges();

                    //reload data to dto
                    dtoColliPiece.ProductColliPieceID = dbColliPiece.ProductColliPieceID;
                    dtoColliPiece.Pcs = dbColliPiece.Pcs;
                    dtoColliPiece.Colli = dbColliPiece.Colli;
                    dtoColliPiece.PieceID = dbColliPiece.PieceID;
                    dtoColliPiece.PiceDescription = context.ProductMng_ProductColliPiece_View.Where(o => o.ProductColliPieceID == dtoColliPiece.ProductColliPieceID).FirstOrDefault().PiceDescription;

                }
                return dtoColliPiece;
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
                return dtoColliPiece;
            }
        }

        public bool DeleteSetEanCode(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Delete set ean code success" };
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    var x = context.ProductSetEANCode.Where(o => o.ProductSetEANCodeID == id).FirstOrDefault();
                    context.ProductSetEANCode.Remove(x);
                    context.ProductSetEANCode.Local.Where(o => o.Product == null).ToList().ForEach(o => context.ProductSetEANCode.Remove(o));
                    context.SaveChanges();
                }
                return true;
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

        public bool DeleteColli(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Delete set colli ean code success" };
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    var x = context.ProductColli.Where(o => o.ProductColliID == id).FirstOrDefault();
                    context.ProductColli.Remove(x);
                    context.ProductColli.Local.Where(o => o.ProductSetEANCode == null).ToList().ForEach(o => context.ProductColli.Remove(o));
                    context.SaveChanges();
                }
                return true;
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

        public bool DeleteColliPiece(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Delete set piece success" };
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    var x = context.ProductColliPiece.Where(o => o.ProductColliPieceID == id).FirstOrDefault();
                    context.ProductColliPiece.Remove(x);
                    context.ProductColliPiece.Local.Where(o => o.ProductColli == null).ToList().ForEach(o => context.ProductColliPiece.Remove(o));
                    context.SaveChanges();
                }
                return true;
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

        public DTO.ProductMng.ProductColliPiece UpdateColliPiece(DTO.ProductMng.ProductColliPiece dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Update colli piece success" };
            try
            {
                using (ProductMngEntities context = CreateContext())
                {
                    var dbColliPiece = context.ProductColliPiece.Where(o => o.ProductColliPieceID == dtoItem.ProductColliPieceID).FirstOrDefault();
                    dbColliPiece.PieceID = dtoItem.PieceID;
                    dbColliPiece.Colli = dtoItem.Colli;
                    dbColliPiece.Pcs = dtoItem.Pcs;
                    dbColliPiece.ColliPieceDescription = dtoItem.ColliPieceDescription;
                    context.SaveChanges();
                }
                return dtoItem;
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
                return dtoItem;
            }
        }

        public string PrintEANCode(int ProductColliPieceID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Print set piece success" };
            return "";
        }
    }
}
