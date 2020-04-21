using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
namespace DAL.ShowroomAreaMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ShowroomAreaMng.SearchFormData, DTO.ShowroomAreaMng.EditFormData, DTO.ShowroomAreaMng.ShowroomArea>
    {
        private DataConverter converter = new DataConverter();

        private string _TempFolder;

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private ShowroomAreaMngEntities CreateContext()
        {
            return new ShowroomAreaMngEntities(DALBase.Helper.CreateEntityConnectionString("ShowroomAreaMngModel"));
        }

        public override bool Approve(int id, ref DTO.ShowroomAreaMng.ShowroomArea dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ShowroomAreaMng.ShowroomArea dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Delete success" };
            try
            {
                using (ShowroomAreaMngEntities context = CreateContext())
                {
                    var dbItem = context.ShowroomArea.Where(o => o.ShowroomAreaID == id).FirstOrDefault();
                    if (dbItem == null)
                    {
                        notification.Message = "Deleted not success. Data not found";
                        return false;
                    }
                    else
                    {
                        context.ShowroomArea.Remove(dbItem);
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

        public override DTO.ShowroomAreaMng.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.ShowroomAreaMng.SearchFormData searchFormData = new DTO.ShowroomAreaMng.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ShowroomAreaUD = string.Empty;
            string ShowroomAreaNM = string.Empty;

            if (filters.ContainsKey("ShowroomAreaUD")) ShowroomAreaUD = filters["ShowroomAreaUD"].ToString();
            if (filters.ContainsKey("ShowroomAreaNM")) ShowroomAreaNM = filters["ShowroomAreaNM"].ToString();

            try
            {
                using (ShowroomAreaMngEntities context = CreateContext())
                {
                    totalRows = context.ShowroomAreaMng_function_SearchShowroomArea(orderBy, orderDirection,ShowroomAreaUD,ShowroomAreaNM).Count();
                    var result = context.ShowroomAreaMng_function_SearchShowroomArea(orderBy, orderDirection, ShowroomAreaUD, ShowroomAreaNM);
                    searchFormData.Data = converter.DB2DTO_ShowroomAreaSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public override DTO.ShowroomAreaMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.ShowroomAreaMng.EditFormData editFormData = new DTO.ShowroomAreaMng.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShowroomAreaMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ShowroomAreaMng_ShowroomArea_View dbItem;
                        dbItem = context.ShowroomAreaMng_ShowroomArea_View
                            .Include("ShowroomAreaMng_ShowroomAreaImage_View")
                            .FirstOrDefault(o => o.ShowroomAreaID == id);

                        editFormData.Data = converter.DB2DTO_ShowroomArea(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.ShowroomAreaMng.ShowroomArea();
                        editFormData.Data.ShowroomAreaImages = new List<DTO.ShowroomAreaMng.ShowroomAreaImage>();
                        editFormData.Data.ShowroomAreaImages.Add(new DTO.ShowroomAreaMng.ShowroomAreaImage { ShowroomAreaImageID = -1, AreaImageUD = "TL", AreaImageNM = "Top Left", ImageFile_HasChange = false});
                        editFormData.Data.ShowroomAreaImages.Add(new DTO.ShowroomAreaMng.ShowroomAreaImage { ShowroomAreaImageID = -2, AreaImageUD = "TR", AreaImageNM = "Top Right", ImageFile_HasChange = false });
                        editFormData.Data.ShowroomAreaImages.Add(new DTO.ShowroomAreaMng.ShowroomAreaImage { ShowroomAreaImageID = -3, AreaImageUD = "BL", AreaImageNM = "Bottom Left", ImageFile_HasChange = false });
                        editFormData.Data.ShowroomAreaImages.Add(new DTO.ShowroomAreaMng.ShowroomAreaImage { ShowroomAreaImageID = -4, AreaImageUD = "BR", AreaImageNM = "Bottom Right", ImageFile_HasChange = false });
                        editFormData.Data.ShowroomAreaImages.Add(new DTO.ShowroomAreaMng.ShowroomAreaImage { ShowroomAreaImageID = -5, AreaImageUD = "MAP", AreaImageNM = "MAP", ImageFile_HasChange = false });
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

        public override bool UpdateData(int id, ref DTO.ShowroomAreaMng.ShowroomArea dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShowroomAreaMngEntities context = CreateContext())
                {
                    ShowroomArea dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ShowroomArea();
                        context.ShowroomArea.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ShowroomArea.FirstOrDefault(o => o.ShowroomAreaID == id);
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
                        converter.DTO2DB_ShowroomArea(dtoItem, ref dbItem, this._TempFolder);
                        //reove orphan
                        context.ShowroomAreaImage.Local.Where(o => o.ShowroomArea == null).ToList().ForEach(o => context.ShowroomAreaImage.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.ShowroomAreaID, out notification).Data;
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
                adap.SelectCommand = new SqlCommand("ShowroomAreaMng_function_GetExportExcel_ShowroomArea", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.TableMappings.Add("Table", "ShowroomAreaMng_ExportExcel_ShowroomArea_View");
                adap.Fill(ds);
                ds.AcceptChanges();

                //foreach (var item in ds.ShowroomAreaMng_ExportExcel_ShowroomArea_View.Where(o => !o.IsShowroomAreaUDNull()))
                //{
                //    item.BarCode = BarCode.BarcodeConverter128.StringToBarcode(item.ShowroomAreaUD);
                //}

                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ShowroomItem");
                //return string.Empty;

                // generate xml file
                string result = DALBase.Helper.CreateReportFiles(ds, "ShowroomArea");
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
    }
}
