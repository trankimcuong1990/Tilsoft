using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
namespace DAL.ShowroomItemMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ShowroomItemMng.SearchFormData, DTO.ShowroomItemMng.EditFormData, DTO.ShowroomItemMng.ShowroomItem>
    {
        private DataConverter converter = new DataConverter();
        private string _TempFolder;

        public DataFactory() { }
        
        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }
        private ShowroomItemMngEntities CreateContext()
        {
            return new ShowroomItemMngEntities(DALBase.Helper.CreateEntityConnectionString("ShowroomItemMngModel"));
        }

        public override bool Approve(int id, ref DTO.ShowroomItemMng.ShowroomItem dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ShowroomItemMng.ShowroomItem dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        
        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Delete success" };
            try
            {
                using (ShowroomItemMngEntities context = CreateContext())
                {
                    var dbItem = context.ShowroomItem.Where(o => o.ShowroomItemID == id).FirstOrDefault();
                    if (dbItem == null)
                    {
                        notification.Message = "Deleted not success. Data not found";
                        return false;
                    }
                    else {
                        context.ShowroomItem.Remove(dbItem);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex){
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

        public override DTO.ShowroomItemMng.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.ShowroomItemMng.SearchFormData searchFormData = new DTO.ShowroomItemMng.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ArticleCode = string.Empty;
            string Description = string.Empty;
            string MaterialNM = string.Empty;
            string MaterialTypeNM = string.Empty;
            string MaterialColorNM = string.Empty;
            string FrameMaterialNM = string.Empty;
            string FrameMaterialColorNM = string.Empty;
            string SubMaterialNM = string.Empty;
            string SubMaterialColorNM = string.Empty;
            string SeatCushionNM = string.Empty;
            string BackCushionNM = string.Empty;
            string CushionColorNM = string.Empty;

            if (filters.ContainsKey("ArticleCode")) ArticleCode = filters["ArticleCode"].ToString();
            if (filters.ContainsKey("Description")) Description = filters["Description"].ToString();
            if (filters.ContainsKey("MaterialNM")) MaterialNM = filters["MaterialNM"].ToString();
            if (filters.ContainsKey("MaterialTypeNM")) MaterialTypeNM = filters["MaterialTypeNM"].ToString();
            if (filters.ContainsKey("MaterialColorNM")) MaterialColorNM = filters["MaterialColorNM"].ToString();
            if (filters.ContainsKey("FrameMaterialNM")) FrameMaterialNM = filters["FrameMaterialNM"].ToString();
            if (filters.ContainsKey("FrameMaterialColorNM")) FrameMaterialColorNM = filters["FrameMaterialColorNM"].ToString();
            if (filters.ContainsKey("SubMaterialNM")) SubMaterialNM = filters["SubMaterialNM"].ToString();
            if (filters.ContainsKey("SubMaterialColorNM")) SubMaterialColorNM = filters["SubMaterialColorNM"].ToString();
            if (filters.ContainsKey("SeatCushionNM")) SeatCushionNM = filters["SeatCushionNM"].ToString();
            if (filters.ContainsKey("BackCushionNM")) BackCushionNM = filters["BackCushionNM"].ToString();
            if (filters.ContainsKey("CushionColorNM")) CushionColorNM = filters["CushionColorNM"].ToString();

            try
            {
                using (ShowroomItemMngEntities context = CreateContext())
                {
                    totalRows = context.ShowroomItemMng_function_SearchShowroomItem(orderBy, orderDirection, ArticleCode, Description, MaterialNM, MaterialTypeNM, MaterialColorNM, FrameMaterialNM, FrameMaterialColorNM, SubMaterialNM, SubMaterialColorNM, SeatCushionNM, BackCushionNM, CushionColorNM).Count();
                    var result = context.ShowroomItemMng_function_SearchShowroomItem(orderBy, orderDirection, ArticleCode, Description, MaterialNM, MaterialTypeNM, MaterialColorNM, FrameMaterialNM, FrameMaterialColorNM, SubMaterialNM, SubMaterialColorNM, SeatCushionNM, BackCushionNM, CushionColorNM);
                    searchFormData.Data = converter.DB2DTO_ShowroomItemSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return searchFormData;
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
                return searchFormData;
            }
        }

        public override DTO.ShowroomItemMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.ShowroomItemMng.EditFormData editFormData = new DTO.ShowroomItemMng.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShowroomItemMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ShowroomItemMng_ShowroomItem_View dbItem;
                        dbItem = context.ShowroomItemMng_ShowroomItem_View
                            .FirstOrDefault(o => o.ShowroomItemID == id);

                        editFormData.Data = converter.DB2DTO_ShowroomItem(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.ShowroomItemMng.ShowroomItem();
                    }
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return editFormData;
            }
        }

        public override bool UpdateData(int id, ref DTO.ShowroomItemMng.ShowroomItem dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShowroomItemMngEntities context = CreateContext())
                {
                    ShowroomItem dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ShowroomItem();
                        context.ShowroomItem.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ShowroomItem.FirstOrDefault(o => o.ShowroomItemID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        //if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        //{
                        //    throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        //}

                        //convert dto to db
                        converter.DTO2DB_ShowroomItem(dtoItem, ref dbItem,this._TempFolder);
                        //save data
                        context.SaveChanges();
                        //generate ArticleCode
                        context.ShowroomItemMng_function_GenerateCode(dbItem.ShowroomItemID);
                        //get return data
                        dtoItem = GetData(dbItem.ShowroomItemID, out notification).Data;
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

        public string GetExportExcelFile(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Export success" };
            try
            {
                ReportDataObject ds = new ReportDataObject();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ShowroomItemMng_function_GetExportExcel_ShowroomItem", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.TableMappings.Add("Table", "ShowroomItemMng_ExportExcel_ShowroomItem_View");
                adap.Fill(ds);
                ds.AcceptChanges();

                //foreach (var item in ds.ShowroomItemMng_ExportExcel_ShowroomItem_View.Where(o => !o.IsArticleCodeNull()))
                //{
                //    item.BarCode = BarCode.BarcodeConverter39.StringToBarcode(item.ArticleCode);
                //    //string qrCode = FrameworkSetting.Setting.ReportTempUrl + Library.Helper.QRCodeImageFile("abcd");
                //}


                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ShowroomItem");
                //return string.Empty;

                // generate xml file
                string result = DALBase.Helper.CreateReportFiles(ds, "ShowroomItem");
                return result;
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
                return string.Empty;
            }
        }

        public List<DTO.ShowroomItemMng.SampleProduct> QuickSearchSampleProduct(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.ShowroomItemMng.SampleProduct> data = new List<DTO.ShowroomItemMng.SampleProduct>();
            try
            {
                using (ShowroomItemMngEntities context = CreateContext())
                {
                    string searchQuery = null;
                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }
                    totalRows = context.ShowroomItem_function_QuickSearchSampleProduct(searchQuery, orderBy, orderDirection).Count();
                    var result = context.ShowroomItem_function_QuickSearchSampleProduct(searchQuery, orderBy, orderDirection);
                    data = converter.DB2DTO_SampleProduct(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
    }
}
