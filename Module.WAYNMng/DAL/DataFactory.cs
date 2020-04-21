using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Net;

namespace Module.WAYNMng.DAL
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
        private WAYNMngEntities CreateContext()
        {
            return new WAYNMngEntities(Library.Helper.CreateEntityConnectionString("DAL.WAYNMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.WAYN>();
            data.FileScan = new DTO.FileScan();
            totalRows = 0;

            //try to get data
            try
            {
                using (WAYNMngEntities context = CreateContext())
                {
                    var _data = context.WAYNMng_WAYN_View.ToList();
                    var result = _data.GroupBy(x => x.WorkingDate).Select(grp => grp.First()).ToList();
                    totalRows = result.Count();
                    data.Data = converter.DB2DTO_WAYNList(result);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int ID, out Library.DTO.Notification notification)
        {
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.EditFormData data = new DTO.EditFormData();
            //data.Data = new DTO.EmployeeList();

            ////try to get data
            //try
            //{
            //    using (WAYNMngEntities context = CreateContext())
            //    {
            //        data.Data = converter.DB2DTO_Employee(context.WAYNMng_EmployeeList_View.FirstOrDefault(o => o.EmployeeID == ID));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WAYNMngEntities context = CreateContext())
                {
                    WAYN dbItem = context.WAYN.FirstOrDefault(o => o.WAYNID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Data not found!");
                    }
                    context.WAYN.Remove(dbItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public string GetExcelReportData(string date, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string Date = dt.ToString("MM/dd/yyyy");

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WAYNMng_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Date", Date);
                adap.Fill(ds);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "AttendanceOverview");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "AttendanceOverview");
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
        public IEnumerable<DTO.EmployeeList> GetDetail(int userId, string date, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //Change to format dd-mm-yyy when testing on local
            DateTime _Date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string Date = _Date.ToString("MM/dd/yyyy");
            try
            {
                using (WAYNMngEntities context = CreateContext())
                {
                    var data = context.WAYNMng_EmployeeList_View.Where(o => o.WorkingDate == _Date);
                    var result = converter.DB2DTO_EmployeeList(data.ToList());
                    return result;
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
                return new List<DTO.EmployeeList>();
            }
        }
        public bool UpdateList(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.EmployeeList> dtoEmployeeList = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.EmployeeList>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            
            try
            {
                foreach (DTO.EmployeeList dtoEmployee in dtoEmployeeList)
                {
                    using (WAYNMngEntities context = CreateContext())
                    {
                        WAYN dbItem = context.WAYN.FirstOrDefault(o => o.WAYNID == dtoEmployee.WAYNID);
                        if (dbItem == null)
                        {
                            throw new Exception("Employee not found !");
                        }

                        converter.DTO2DB(dtoEmployee, ref dbItem);

                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public bool DeleteList(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.EmployeeList> dtoEmployeeList = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.EmployeeList>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                foreach (DTO.EmployeeList dtoEmployee in dtoEmployeeList)
                {
                    using (WAYNMngEntities context = CreateContext())
                    {
                        WAYN dbItem = context.WAYN.FirstOrDefault(o => o.WAYNID == dtoEmployee.WAYNID);
                        if (dbItem == null)
                        {
                            throw new Exception("Employee not found !");
                        }

                        context.WAYN.Remove(dbItem);
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public bool UpdateNewList(int userId, object dtoItem, string date, out Library.DTO.Notification notification)
        {
            List<DTO.EmployeeList> dtoEmployeeList = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.EmployeeList>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            if (dtoEmployeeList != null)
            {
                try
                {
                    foreach (DTO.EmployeeList dtoEmployee in dtoEmployeeList)
                    {
                        using (WAYNMngEntities context = CreateContext())
                        {
                            WAYN dbItem = null;
                            dbItem = new WAYN();
                            context.WAYN.Add(dbItem);
                            dtoEmployee.WorkingDate = date;
                            converter.DTO2DB(dtoEmployee, ref dbItem);
                            LeaveRequest dbLeaveRequest = null;
                            dbLeaveRequest = new LeaveRequest();
                            dbLeaveRequest = context.LeaveRequest.FirstOrDefault(o => o.RequesterID == dbItem.EmployeeID);
                            if(dbLeaveRequest != null)
                            {
                                if (dbItem.WorkingDate >= dbLeaveRequest.FromDate && dbItem.WorkingDate <= dbLeaveRequest.ToDate)
                                {
                                    dbItem.IsOutOfOffice = true;
                                    dbItem.LeaveTypeID = dbLeaveRequest.LeaveTypeID;
                                    if (dbLeaveRequest.TotalDays == (decimal)0.5)
                                    {
                                        dbItem.IsHaftDayOff = true;
                                    }
                                    dbItem.Description = dbLeaveRequest.ReasonForLeave;
                                }
                            }
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;
                            context.SaveChanges();
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = ex.Message;
                    return false;
                }
            }
            else
            {
                throw new Exception("Cannot find file location !");
            }
        }
    }
    //public static class ExcelPackageExtensions
    //{
    //    public static DataTable getDataTableFromExcel(string path)
    //    {
    //        using (var pck = new OfficeOpenXml.ExcelPackage())
    //        {
    //            var uri = new Uri(path);
    //            var location = uri.AbsolutePath;

    //            var existingFile = new FileInfo("C:/DATA/Tilsoft/SOLUTION/TilsoftService/" + location);

    //            using (var stream = File.OpenRead(existingFile.ToString()))
    //            {
    //                pck.Load(stream);
    //            }

    //            var ws = pck.Workbook.Worksheets.First();
    //            DataTable tbl = new DataTable();
    //            bool hasHeader = true; // adjust it accordingly( i've mentioned that this is a simple approach)
    //            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
    //            {
    //                tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
    //            }
    //            var startRow = hasHeader ? 2 : 1;
    //            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
    //            {
    //                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
    //                var row = tbl.NewRow();
    //                foreach (var cell in wsRow)
    //                {
    //                    row[cell.Start.Column - 1] = cell.Text;
    //                }
    //                tbl.Rows.Add(row);
    //            }
    //            return tbl;
    //        }
    //    }
    //}
}
