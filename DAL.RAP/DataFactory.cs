using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Linq;
using DALBase;
using DTO.Framework;
using DTO.RAP;
using FrameworkSetting;

namespace DAL.RAP
{
    public class DataFactory
    {
        private string _tempFolder;
        private readonly DataConverter _converter = new DataConverter();

        //internal RAPEntities CreateRapEntity()
        //{
        //    var conn = Helper.CreateEntityConnectionString("RAPModel");
        //    return new RAPEntities(conn);
        //}
        public DataFactory(string tempFolder)
        {
            _tempFolder = tempFolder + @"\"; 
        }


        public string GetRapReportName()
        {
            var ds = GetRapReportDataSet();
            //return Helper.CreateReportFiles(ds, "RapReportList");
            return CreateReportRapFile(ds, "RapReportList");
        }

        static ReportRapDataObject GetRapReportDataSet()
        {
            try
            {
                var ds = new ReportRapDataObject();
                var reportHeader = ds.RapReportHeader.NewRapReportHeaderRow();
                reportHeader.RapReportHeader = "RAP Report List";
                ds.RapReportHeader.AddRapReportHeaderRow(reportHeader);

                var adap = new SqlDataAdapter()
                {
                    SelectCommand =
                        new SqlCommand("SELECT TOP 1000 * FROM dbo.RAP_View",
                            new SqlConnection(Helper.GetSQLConnectionString()))
                        {
                            CommandType = CommandType.Text
                        }
                };
                //adap.TableMappings.Add("Table", "Header");
                //adap.TableMappings.Add("Table1", "Report_RAP_List");
                adap.Fill(ds.RAP_View);

                // dev
                //var serverPathReport = HttpContext.Current.Server.MapPath("/Tmp/Dev/");
                //var xmlTemplate = serverPathReport + "RapReportList.xml";
                //var xsdTemplate = serverPathReport + "RapReportList.xsd";
                //if (File.Exists(xmlTemplate) && File.Exists(xsdTemplate))
                //{
                //    File.Delete(xmlTemplate);
                //    File.Delete(xsdTemplate);
                //    Helper.DevCreateReportXMLSource(ds, "RapReportList");
                //}

                DataTable table = ds.Tables[0];


                return ds;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return new ReportRapDataObject();
            }
           
        }

        static string CreateReportRapFile(DataSet ds, string reportFileName)
        {
            var filename = Guid.NewGuid().ToString().Replace("-", "");
            //filename = "xmlRAP";
            var serverPathReport = HttpContext.Current.Server.MapPath("/Reports/");
            var serverPathTmpFolder = HttpContext.Current.Server.MapPath("/Tmp/");
            var fullpath = serverPathTmpFolder + filename;

            try
            {
                ds.WriteXml(fullpath + ".xml");
                var sourceFile = serverPathReport + reportFileName + ".xlsm";
                var destinationFile = fullpath + ".xlsm";

                if (string.IsNullOrEmpty(sourceFile)) return string.Empty;

                File.Copy(sourceFile, destinationFile);

                return filename;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return string.Empty;
            }
        }
        public List<RapDto> GetRapListReport(out Notification notification)
        {
            try
            {
                using (var context = new RAPEntities())
                {
                    var list = new List<RAP_View>();
                    var listRap = _converter.DB2DTO_RAP(list);
                    notification = new Notification { Message = "success", Type = NotificationType.Success };
                    return listRap;
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Message = ex.Message, Type = NotificationType.Error }; 
                return new List<RapDto>();
            }
        }

        public List<RapDto> GetListArticles(string article)
        {
            try
            {
                using (var context = new RAPEntities())
                {
                    List<RAP_View> list = context.RAP_View.Where(a => a.ArticleCode == article).ToList();
                    List<RapDto> listRap = _converter.DB2DTO_RAP(list);
                    return listRap;
                }
            }
            catch (Exception ex)
            {
                new Notification { Message = ex.Message };
                return new List<RapDto>();
            }
        }

        public List<RapDto> GetListCustomer(string customer)
        {
            try
            {
                using (var context = new RAPEntities())
                {
                    List<RAP_View> list = context.RAP_View.Where(c => c.ClientNM == customer).ToList();
                    List<RapDto> listRap = _converter.DB2DTO_RAP(list);
                    return listRap;
                }
            }
            catch (Exception ex)
            {
                new Notification { Message = ex.Message };
                return new List<RapDto>();
            } 
        }

        public List<RapDto> GetListSale(string sale)
        {
            try
            {
                using (var context = new RAPEntities())
                {
                    List<RAP_View> list = context.RAP_View.Where(s => s.SaleNM == sale).ToList();
                    List<RapDto> listRap = _converter.DB2DTO_RAP(list);
                    return listRap;
                }
            }
            catch (Exception ex)
            {
                new Notification { Message = ex.Message };
                return new List<RapDto>();
            } 
        }

    }
}
