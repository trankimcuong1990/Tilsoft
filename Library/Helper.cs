using System.Data.Entity.Core.EntityClient;
using System.Linq;
using AutoMapper;
using FrameworkSetting;
using System.Data;
using System.Xml;
using System;
using System.Collections.Generic;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity.Validation;

namespace Library
{
    public static class Helper
    {
        public static List<Base.IExecutor> CachedDynamicObject;
        public static List<Base.IExecutor2> CachedDynamicObject2;
        private static System.Globalization.CultureInfo _FrontEndCulture = new System.Globalization.CultureInfo("vi-VN");

        public static string TEXT_CONCURRENCY_CONFLICT = "The data has been changed sinced the last time it was loaded into your screen, please reload";
        public static string TEXT_CONFIRMED_CONFLICT = "Can not make change to a confirmed object !";
        public static string TEXT_UPDATED_UNSUCCESS_NOTFOUND = "Not found data to update";

        public static string TEXT_STATUS_PENDING = "PENDING";
        public static string TEXT_STATUS_CONFIRMED = "CONFIRMED";
        public static string TEXT_STATUS_CANCELED = "CANCELED";
        public static string TEXT_STATUS_REVISED = "REVISED";
        public static string TEXT_STATUS_REVISION_CONFIRMED = "REVISION CONFIRMED";
        public static string TEXT_STATUS_CREATED = "CREATED";

        public static int CREATED = 1;
        public static int UPDATE = 2;
        public static int CONFIRMED = 3;
        public static int REVISED = 4;
        public static int REVISION_CONFIRMED = 5;
        public static int CANCEL = 6;
        public static int ACKNOWLEDGE = 7;

        public static int FOB_INVOICE = 1;
        public static int WAREHOUSE_INVOICE = 2;
        public static int OTHER_INVOICE = 3;
        public static int CREDITNOTE_INVOICE = 4;

        public static string COST_TYPE_DISCOUNT = "D";
        public static string COST_TYPE_SEA_FREIGHT = "S";
        public static string COST_TYPE_TRUCKING = "T";
        public static string COST_TYPE_OTHER = "O";
        public static string COST_TYPE_WAREHOUSE = "W";

        public static string TEXT_INVOICE_DONE_PAYMENT = "DONE PAYMENT";
        public static string TEXT_INVOICE_NOT_DONE_PAYMENT = "NOT DONE PAYMENT";
        public static string LABEL_INVOICE_DONE_PAYMENT = "Done payment";
        public static string LABEL_INVOICE_UNDO_DONE_PAYMENT = "Undo done payment";

        
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType && x.DestinationType == destinationType);
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }

        public static string CreateEntityConnectionString(string edmxFileName)
        {
            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityConnBuilder = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = Setting.SqlConnection,
                Metadata = @"res://*/{{edmx}}.csdl|res://*/{{edmx}}.ssdl|res://*/{{edmx}}.msl".Replace("{{edmx}}", edmxFileName)
            };

            //Set the provider name.

            // Set the provider-specific connection string.

            // Set the Metadata location.
            return entityConnBuilder.ToString();
        }

        public static string GetSQLConnectionString()
        {
            return Setting.SqlConnection;
        }

        public static string CreateReportFiles(DataSet ds, string reportFileName)
        {
            string filename = System.Guid.NewGuid().ToString().Replace("-", "");
            string fullpath = Setting.AbsoluteReportTmpFolder + filename;

            try
            {
                ds.WriteXml(fullpath + ".xml");
                System.IO.File.Copy(Setting.AbsoluteReportFolder + reportFileName + ".xlsm", fullpath + ".xlsm");

                return filename;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string CreateCOMReportFile(DataSet ds, string reportFileName)
        {
            string filename = string.Empty;
            string errMsg = string.Empty;
            Task task1 = Task.Factory.StartNew(() =>
            {
                try
                {
                    string uniquePart = System.Guid.NewGuid().ToString().Replace("-", "");
                    string nameOnly = uniquePart + ".xlsm";
                    string newFile = Setting.AbsoluteReportTmpFolder + nameOnly;
                    string newOutputFile = Setting.AbsoluteReportTmpFolder + uniquePart + ".xlsx";
                    System.IO.File.Copy(Setting.AbsoluteReportFolder + reportFileName + ".xlsm", newFile);

                    object oMissing = System.Reflection.Missing.Value;
                    Excel.XmlMap xlMap;
                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkBook;
                    xlApp.EnableEvents = false;
                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(newFile);
                    xlWorkBook.XmlImportXml(ds.GetXml(), out xlMap, oMissing, oMissing);
                    xlApp.Run(nameOnly + "!ThisWorkbook.OnInit");
                    xlWorkBook.SaveAs(newOutputFile, Excel.XlFileFormat.xlWorkbookDefault, oMissing, oMissing, oMissing, oMissing, Excel.XlSaveAsAccessMode.xlExclusive, oMissing, oMissing, oMissing, oMissing, oMissing);
                    xlWorkBook.Close(false);
                    xlApp.Quit();

                    // clean up
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                    try
                    {
                        System.IO.File.Delete(newFile);
                    }
                    catch { }

                    filename = uniquePart + ".xlsx";
                }
                catch (Exception ex)
                {
                    filename = string.Empty;
                    errMsg = ex.Message;
                    Exception innerEx = ex.InnerException;
                    while (innerEx != null)
                    {
                        errMsg += "\n" + innerEx.Message;
                        innerEx = innerEx.InnerException;
                    }
                }
            });
            Task.WaitAll(task1);

            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }

            return filename;
        }

        public static string CreateCOMReportFileImportDataOnly(DataSet ds, string reportFileName)
        {
            string filename = string.Empty;
            string errMsg = string.Empty;
            Task task1 = Task.Factory.StartNew(() =>
            {
                try
                {
                    string uniquePart = System.Guid.NewGuid().ToString().Replace("-", "");
                    string nameOnly = uniquePart + ".xlsm";
                    string newFile = Setting.AbsoluteReportTmpFolder + nameOnly;
                    string newOutputFile = Setting.AbsoluteReportTmpFolder + uniquePart + ".xlsx";
                    System.IO.File.Copy(Setting.AbsoluteReportFolder + reportFileName + ".xlsm", newFile);

                    object oMissing = System.Reflection.Missing.Value;
                    Excel.XmlMap xlMap;
                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkBook;
                    xlApp.EnableEvents = false;
                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(newFile);
                    xlWorkBook.XmlImportXml(ds.GetXml(), out xlMap, oMissing, oMissing);
                    xlWorkBook.Save();
                    xlWorkBook.Close(false);
                    xlApp.Quit();

                    // clean up
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);

                    filename = nameOnly;
                }
                catch (Exception ex)
                {
                    filename = string.Empty;
                    errMsg = ex.Message;
                    Exception innerEx = ex.InnerException;
                    while (innerEx != null)
                    {
                        errMsg += "\n" + innerEx.Message;
                        innerEx = innerEx.InnerException;
                    }
                }
            });
            Task.WaitAll(task1);

            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }

            return filename;
        }

        public static string CreateCOMReportFileImportDataOnly2(DataSet ds, string reportFileName)
        {
            string filename = string.Empty;
            string errMsg = string.Empty;
            Task task1 = Task.Factory.StartNew(() =>
            {
                try
                {
                    string uniquePart = System.Guid.NewGuid().ToString().Replace("-", "");
                    string nameOnly = uniquePart + ".xlsm";
                    string newFile = Setting.AbsoluteReportTmpFolder + nameOnly;
                    string newOutputFile = Setting.AbsoluteReportTmpFolder + uniquePart + ".xlsx";
                    System.IO.File.Copy(Setting.AbsoluteReportFolder + reportFileName + ".xlsm", newFile);

                    object oMissing = System.Reflection.Missing.Value;
                    Excel.XmlMap xlMap;
                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkBook;
                    xlApp.EnableEvents = false;
                    xlWorkBook = xlApp.Workbooks.Open(newFile);
                    xlWorkBook.XmlImportXml(ds.GetXml(), out xlMap, oMissing, oMissing);
                    xlApp.Run(nameOnly + "!ThisWorkbook.OnInit");
                    xlWorkBook.Save();
                    xlWorkBook.Close(false);
                    xlApp.Quit();

                    // clean up
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);

                    filename = nameOnly;
                }
                catch (Exception ex)
                {
                    filename = string.Empty;
                    errMsg = ex.Message;
                    Exception innerEx = ex.InnerException;
                    while (innerEx != null)
                    {
                        errMsg += "\n" + innerEx.Message;
                        innerEx = innerEx.InnerException;
                    }
                }
            });
            Task.WaitAll(task1);

            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }

            return filename;
        }

        public static string CreateReportFileWithEPPlus(DataSet ds, string reportFileName)
        {
            string result = string.Empty;
            try
            {
                string uniqueFileName = System.Guid.NewGuid().ToString().Replace("-", "") + ".xlsm";
                System.IO.File.Copy(Setting.AbsoluteReportFolder + reportFileName + ".xlsm", Setting.AbsoluteReportTmpFolder + uniqueFileName);

                // remove readonly if needed
                System.IO.FileAttributes att = System.IO.File.GetAttributes(Setting.AbsoluteReportTmpFolder + uniqueFileName);
                File.SetAttributes(Setting.AbsoluteReportTmpFolder + uniqueFileName, att & ~FileAttributes.ReadOnly);

                System.IO.FileInfo fInfo = new System.IO.FileInfo(Setting.AbsoluteReportTmpFolder + uniqueFileName);
                OfficeOpenXml.ExcelPackage ePackagae = new OfficeOpenXml.ExcelPackage(fInfo);
                OfficeOpenXml.ExcelWorksheet eWorkSheet;
                int eWorkSheetIndex = 1;
                foreach (DataTable table in ds.Tables)
                {
                    eWorkSheet = ePackagae.Workbook.Worksheets["Data" + eWorkSheetIndex.ToString()];
                    var eRange = eWorkSheet.Cells["A1"].LoadFromDataTable(table, true);
                    eWorkSheetIndex++;
                }
                ePackagae.Save();

                result = uniqueFileName;
            }
            catch { }

            return result;
        }

        public static string CreateReportFileWithEPPlus(DataSet ds, string reportFileName, List<string> tableList)
        {
            string result = string.Empty;
            try
            {
                string uniqueFileName = System.Guid.NewGuid().ToString().Replace("-", "") + ".xlsm";
                System.IO.File.Copy(Setting.AbsoluteReportFolder + reportFileName + ".xlsm", Setting.AbsoluteReportTmpFolder + uniqueFileName);

                // remove readonly if needed
                System.IO.FileAttributes att = System.IO.File.GetAttributes(Setting.AbsoluteReportTmpFolder + uniqueFileName);
                File.SetAttributes(Setting.AbsoluteReportTmpFolder + uniqueFileName, att & ~FileAttributes.ReadOnly);

                System.IO.FileInfo fInfo = new System.IO.FileInfo(Setting.AbsoluteReportTmpFolder + uniqueFileName);
                OfficeOpenXml.ExcelPackage ePackagae = new OfficeOpenXml.ExcelPackage(fInfo);
                OfficeOpenXml.ExcelWorksheet eWorkSheet;
                int eWorkSheetIndex = 1;
                foreach (string tableName in tableList)
                {
                    eWorkSheet = ePackagae.Workbook.Worksheets["Data" + eWorkSheetIndex.ToString()];
                    var eRange = eWorkSheet.Cells["A1"].LoadFromDataTable(ds.Tables[tableName], true);
                    eWorkSheetIndex++;
                }
                ePackagae.Save();

                result = uniqueFileName;
            }
            catch (Exception ex) { }

            return result;
        }

        public static string ExportDataSetWithEPPlus(DataSet ds, string reportFileName)
        {
            string result = string.Empty;
            try
            {
                System.IO.FileInfo fInfo = new System.IO.FileInfo(reportFileName);
                OfficeOpenXml.ExcelPackage ePackagae = new OfficeOpenXml.ExcelPackage(fInfo);
                OfficeOpenXml.ExcelWorksheet eWorkSheet;
                int eWorkSheetIndex = 1;
                foreach (DataTable dtItem in ds.Tables)
                {
                    eWorkSheet = ePackagae.Workbook.Worksheets.Add(dtItem.TableName);
                    var eRange = eWorkSheet.Cells["A1"].LoadFromDataTable(dtItem, true);
                    eWorkSheetIndex++;
                }
                ePackagae.Save();
            }
            catch (Exception ex) { }

            return result;
        }

        public static string CreateReportFileWithEPPlus2(DataSet ds, string reportFileName)
        {
            string result = string.Empty;
            string uniqueFileName = System.Guid.NewGuid().ToString().Replace("-", "") + ".xlsm";

            // Issue from mr.Long change file name by get yyyymmddhhmmssff
            if ("RAPVN".Equals(reportFileName) || "RAPEU".Equals(reportFileName))
            {
                uniqueFileName = reportFileName + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmssff") + ".xlsm";
            }

            System.IO.File.Copy(Setting.AbsoluteReportFolder + reportFileName + ".xlsm", Setting.AbsoluteReportTmpFolder + uniqueFileName);

            // remove readonly if needed
            System.IO.FileAttributes att = System.IO.File.GetAttributes(Setting.AbsoluteReportTmpFolder + uniqueFileName);
            File.SetAttributes(Setting.AbsoluteReportTmpFolder + uniqueFileName, att & ~FileAttributes.ReadOnly);

            System.IO.FileInfo fInfo = new System.IO.FileInfo(Setting.AbsoluteReportTmpFolder + uniqueFileName);
            OfficeOpenXml.ExcelPackage ePackagae = new OfficeOpenXml.ExcelPackage(fInfo);
            OfficeOpenXml.ExcelWorksheet eWorkSheet;
            string sheetName = "";
            foreach (DataTable table in ds.Tables)
            {
                sheetName = table.TableName;
                if (sheetName.Length > 32)
                {
                    sheetName = table.TableName.Substring(0, 31);
                }
                //eWorkSheet = ePackagae.Workbook.Worksheets.Add(sheetName);
                eWorkSheet = ePackagae.Workbook.Worksheets[sheetName];
                var eRange = eWorkSheet.Cells["A1"].LoadFromDataTable(table, true);
            }
            ePackagae.Save();
            result = uniqueFileName;
            return result;
        }

        public static string CreateExcelFileWithEPPlus<Unknow>(List<Unknow> data)
        {
            string result = string.Empty;
            string uniqueFileName = System.Guid.NewGuid().ToString().Replace("-", "") + ".xlsx";
            System.IO.FileInfo f = new System.IO.FileInfo(Setting.AbsoluteReportTmpFolder + uniqueFileName);
            if (f.Exists) f.Delete();
            using (OfficeOpenXml.ExcelPackage ep = new OfficeOpenXml.ExcelPackage(f))
            {
                OfficeOpenXml.ExcelWorksheet eSheet = ep.Workbook.Worksheets.Add("Data");
                eSheet.Cells[2, 1].LoadFromCollection(data);
                ep.Save();
            }            
            result = uniqueFileName;
            return result;
        }

        public static DateTime? ConvertStringToDateTime(this string dateString)
        {
            return dateString.ConvertStringToDateTime(_FrontEndCulture);
        }
        public static DateTime? ConvertStringToDateTime(this string dateString, System.Globalization.CultureInfo culture)
        {
            DateTime tmpDate;
            if (!string.IsNullOrEmpty(dateString) && DateTime.TryParse(dateString, culture, System.Globalization.DateTimeStyles.None, out tmpDate))
            {
                return tmpDate;
            }

            return null;
        }
       
        public static void DevCreateReportXMLSource(DataSet ds, string name)
        {
            string pathToExport = Setting.AbsoluteReportTmpFolder + "Dev/";

            XmlDocument xsdDoc = new XmlDocument();
            xsdDoc.LoadXml(ds.GetXmlSchema());
            xsdDoc.Save(pathToExport + name + ".xsd");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ds.GetXml());
            xmlDoc.Save(pathToExport + name + ".xml");
        }

        public static string GetCurrentSeason()
        {
            int curYear = DateTime.Now.Year;
            if (DateTime.Now.Month >= 9)
            {
                return curYear.ToString() + "/" + (curYear + 1).ToString();
            }
            return (curYear - 1).ToString() + "/" + curYear.ToString();
        }

        public static string GetPreviousSeason(string season)
        {
            if (season.Length != 9) return string.Empty;

            return (Convert.ToInt32(season.Substring(0, 4)) - 1).ToString() + "/" + season.Substring(0, 4);
        }

        public static string GetNextSeason(string season)
        {
            int year = 0;
            if (int.TryParse(season.Substring(0, 4), out year) && year > 2010)
            {
                return (year + 1).ToString() + "/" + (year + 2).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static Base.IExecutor GetDynamicObject(string TypeDesriptor)
        {
            if (CachedDynamicObject == null)
            {
                CachedDynamicObject = new List<Base.IExecutor>();
            }

            Base.IExecutor result = CachedDynamicObject.FirstOrDefault(o => o.identifier == TypeDesriptor);
            if (result != null)
            {
                return result;
            }
            else
            {
                result = (Base.IExecutor)Activator.CreateInstance(Type.GetType(TypeDesriptor));
                result.identifier = TypeDesriptor;
                CachedDynamicObject.Add(result);
                return result;
            }
        }

        public static Base.IExecutor2 GetDynamicObject2(string TypeDesriptor)
        {
            if (CachedDynamicObject2 == null)
            {
                CachedDynamicObject2 = new List<Base.IExecutor2>();
            }

            Base.IExecutor2 result = CachedDynamicObject2.FirstOrDefault(o => o.identifier == TypeDesriptor);
            if (result != null)
            {
                return result;
            }
            else
            {
                result = (Base.IExecutor2)Activator.CreateInstance(Type.GetType(TypeDesriptor));
                result.identifier = TypeDesriptor;
                CachedDynamicObject2.Add(result);
                return result;
            }
        }

        public static string QRCodeImageFile(string content) {
            try
            {
                QRCodeEncoder encoder = new QRCodeEncoder();
                Bitmap imgQRCode = encoder.Encode(content);

                string filename = System.Guid.NewGuid().ToString().Replace("-", "");
                string fullpath = Setting.AbsoluteReportTmpFolder + filename + ".png";
                imgQRCode.Save(fullpath, ImageFormat.Png);
                return filename + ".png";
            }
            catch {
                return string.Empty;
            }
        }

        public static string GetSeason(DateTime input)
        {
            if (input.Month >= 10)
            {
                return input.Year.ToString() + "/" + (input.Year + 1).ToString();
            }
            else
            {
                return (input.Year - 1).ToString() + "/" + input.Year.ToString();
            }
        }

        //
        // helper function
        //
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public static string CreateCSVFile(DataTable dtData)
        {
            string filename = System.Guid.NewGuid().ToString().Replace("-", "") + ".csv";
            string fullpath = Setting.AbsoluteReportTmpFolder + filename;
            try
            {
                dtData.ToCSV(fullpath);
                return filename;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(";");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(';'))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static string getUniqueFileName(string path, string fileName)
        {
            FileInfo fInfo = new FileInfo(fileName);
            string extensionPart = fInfo.Extension;
            string namePart = fInfo.Name.Replace(fInfo.Extension, "");
            int fileIndex = 1;

            while (File.Exists(path + fileName))
            {
                fileName = namePart + "_" + fileIndex.ToString() + extensionPart;
                fileIndex++;
            }
            return fileName;
        }

        private static string BuildPrintoutFile(LocalReport lr, string fileTypeID)
        {
            string reportType = fileTypeID;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + fileTypeID + "</OutputFormat>" +            
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            //SAVE BYTE ARRAY TO FILE
            string filename = System.Guid.NewGuid().ToString().Replace("-", "");
            switch (fileTypeID)
            {
                case "PDF":
                    filename = filename + ".pdf";
                    break;
                case "Excel":
                    filename = filename + ".xls";
                    break;
                case "Word":
                    filename = filename + ".doc";
                    break;
                case "Image":
                    filename = filename + ".jpg";
                    break;
                default:
                    break;
            }
            string fullpath = Setting.AbsoluteReportTmpFolder + filename;

            MemoryStream ms = new MemoryStream(renderedBytes);
            FileStream fs = File.OpenWrite(fullpath);
            ms.WriteTo(fs);
            fs.Close();

            return filename;
        }

        public static string CreateReceiptPrintout(DataTable dtData, DataTable dtDetail, string reportName)
        {
            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + reportName;

            lr.EnableExternalImages = true;

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoice.Name = dtData.TableName;
            rsInvoice.Value = dtData;
            lr.DataSources.Add(rsInvoice);

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoiceDetail.Name = dtDetail.TableName;
            rsInvoiceDetail.Value = dtDetail;
            lr.DataSources.Add(rsInvoiceDetail);

            return BuildPrintoutFile(lr, "PDF");
        }

        public static int GetCurrentWeek()
        {
            System.Globalization.DateTimeFormatInfo dfi = (new System.Globalization.CultureInfo("nl-NL")).DateTimeFormat;
            System.Globalization.Calendar calendar = dfi.Calendar;
            int weekNo = calendar.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNo;
        }

        public static Exception GetInnerException(Exception ex)
        {
            Exception iEx = ex;
            while (iEx.InnerException != null)
            {
                iEx = iEx.InnerException;
            }
            return iEx;
        }

        public static List<int> GetMonths()
        {
            List<int> months = new List<int>();
            for (var i = 1; i <= 12; i++) {
                months.Add(i);
            }
            return months;
        }

        public static List<int> GetYears()
        {
            List<int> years = new List<int>();
            for (var i = 2015; i <= 2025; i++)
            {
                years.Add(i);
            }
            return years;
        }

        public static string ToStringDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy hh:mm tt");
        }

        public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }

        public static List<string> HandleException(Exception ex, DataSet ds = null)
        {
            //var errorType = ex.GetType();
            List<string> errors = new List<string>();
            string errorDescription = string.Empty;
            if (ex is DbUpdateException)
            {
                // unique key constrain violation
                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    if (ex.InnerException.InnerException is SqlException sqlException)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:  // Unique constraint error
                            case 547:   // Constraint check violation
                            case 2601:  // Duplicated key row error
                                errors.Add("Unique violation: " + sqlException.Message);
                                break;

                            default:
                                errors.Add(sqlException.Message);
                                break;
                        }
                    }
                }
            }
            else if (ex is DbEntityValidationException entityException)
            {
                foreach (var entityError in entityException.EntityValidationErrors)
                {
                    errorDescription = "Error " + entityError.Entry.State.ToString() + " " + entityError.Entry.Entity.GetType().Name + ": ";
                    foreach (var validationError in entityError.ValidationErrors)
                    {
                        errorDescription += validationError.PropertyName + " - " + validationError.ErrorMessage;
                    }
                    errors.Add(errorDescription);
                }
            }
            else if (ex is ConstraintException dsException)
            {
                if (ds != null)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt.HasErrors)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr.HasErrors)
                                {
                                    errors.Add("Error: " + dr.RowError);
                                }
                            }
                        }
                    }
                }

                if (errors.Count() == 0)
                {
                    errors.Add("Dataset error: " + dsException.Message);
                }
            }
            else
            {
                errors.Add("Error: " + ex.Message);
            }
            return errors;
        }

        public static bool? ConvertStringToBool(this string boolString)
        {
            bool tmpValue;
            if (!string.IsNullOrEmpty(boolString) && Boolean.TryParse(boolString, out tmpValue))
            {
                return tmpValue;
            }
            return null;
        }
        public static int? ConvertStringToInt(this string intString)
        {
            int tmpValue;
            if (!string.IsNullOrEmpty(intString) && int.TryParse(intString, out tmpValue))
            {
                return tmpValue;
            }
            return null;
        }
        public static decimal? ConvertStringToDecimal(this string decString, System.Globalization.CultureInfo culture)
        {
            decimal tmpValue;
            if (!string.IsNullOrEmpty(decString) && decimal.TryParse(decString, System.Globalization.NumberStyles.AllowDecimalPoint, culture, out tmpValue))
            {
                return tmpValue;
            }
            return null;
        }

        public static string HandleExceptionSingleLine(Exception ex, DataSet ds = null)
        {
            string result = string.Empty;
            foreach (string errorLine in HandleException(ex, ds))
            {
                result += errorLine + Environment.NewLine;
            }
            return result;
        }

        #region convert list to dataset
        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }
        #endregion

        #region Create Report PDF with Data set //NL
        public static string CreateReceiptPrintout2(DataSet ds, string reportName)
        {
            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.DataSources.Clear();
            lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + reportName;
            //Emnable Image
            lr.EnableExternalImages = true;
            //
            foreach (DataTable dt in ds.Tables)
            {
                Microsoft.Reporting.WebForms.ReportDataSource rs = new Microsoft.Reporting.WebForms.ReportDataSource();
                rs.Name = dt.TableName;
                rs.Value = dt;
                lr.DataSources.Add(rs);
            }
            return BuildPrintoutFile(lr, "PDF");
        }
        #endregion
    }
}